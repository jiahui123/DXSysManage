namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("mail", "subject", c => c.String(nullable: false, maxLength: 50000, unicode: false, storeType: "nvarchar"));
            AlterColumn("mail", "sendAddress", c => c.String(nullable: false, maxLength: 5000, unicode: false, storeType: "nvarchar"));
            AlterColumn("mail", "MailBody", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("mail", "MailBody", c => c.String(maxLength: 50000, unicode: false, storeType: "nvarchar"));
            AlterColumn("mail", "sendAddress", c => c.String(nullable: false, maxLength: 500, unicode: false, storeType: "nvarchar"));
            AlterColumn("mail", "subject", c => c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"));
        }
    }
}
