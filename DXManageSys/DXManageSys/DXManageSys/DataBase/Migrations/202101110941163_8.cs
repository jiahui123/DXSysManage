namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "mail",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        subject = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        mailUID = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        sendAddress = c.String(nullable: false, maxLength: 500, unicode: false, storeType: "nvarchar"),
                        MailBody = c.String(maxLength: 50000, unicode: false, storeType: "nvarchar"),
                        Attachment = c.String(maxLength: 50000, unicode: false, storeType: "nvarchar"),
                        creatDate = c.DateTime(nullable: false, precision: 0),
                        creatUser = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        dataStaus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)                ;
            
        }
        
        public override void Down()
        {
            DropTable("mail");
        }
    }
}
