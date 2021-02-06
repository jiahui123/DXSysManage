namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("user", "dataStaus1", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("user", "dataStaus1");
        }
    }
}
