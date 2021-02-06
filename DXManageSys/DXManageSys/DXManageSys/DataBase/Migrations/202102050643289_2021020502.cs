namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2021020502 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pl_head", "total_qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_cin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_nkgs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_gkgs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_cbm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("pl_head", "total_cbm", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_gkgs", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_nkgs", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_cin", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_qty", c => c.String(nullable: false, unicode: false));
        }
    }
}
