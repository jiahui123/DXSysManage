namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("user", "dataStaus1");
        }
        
        public override void Down()
        {
            AddColumn("user", "dataStaus1", c => c.Int(nullable: false));
        }
    }
}
