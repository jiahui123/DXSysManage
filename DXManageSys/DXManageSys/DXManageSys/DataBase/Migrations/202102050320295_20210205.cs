namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210205 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pl_cd",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        bgNumber = c.String(nullable: false, unicode: false),
                        jianshu = c.Decimal(nullable: false, precision: 18, scale: 2),
                        maozhong = c.Decimal(nullable: false, precision: 18, scale: 2),
                        tiji = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)                ;
            
            AlterColumn("pl_head", "total_qty", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_cin", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_nkgs", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_gkgs", c => c.String(nullable: false, unicode: false));
            AlterColumn("pl_head", "total_cbm", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("pl_head", "total_cbm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_gkgs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_nkgs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_cin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("pl_head", "total_qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("pl_cd");
        }
    }
}
