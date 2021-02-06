namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2021020501 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pl_cd", "fileName", c => c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("pl_cd", "fileName");
        }
    }
}
