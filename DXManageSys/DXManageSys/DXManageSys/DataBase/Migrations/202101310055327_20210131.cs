namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210131 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pl_detail",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        pl_head_id = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        goods = c.String(nullable: false, maxLength: 800, unicode: false, storeType: "nvarchar"),
                        qty = c.Int(nullable: false),
                        cin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        nkgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        gkgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        cbm = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)                
                .ForeignKey("pl_head", t => t.pl_head_id, cascadeDelete: true)
                .Index(t => t.pl_head_id);
            
            CreateTable(
                "pl_head",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        invoice = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        order_no = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        date = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        cif = c.String(nullable: false, maxLength: 100, unicode: false, storeType: "nvarchar"),
                        total_qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total_cin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total_nkgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total_gkgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        total_cbm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        english = c.String(nullable: false, maxLength: 500, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)                ;
            
        }
        
        public override void Down()
        {
            DropForeignKey("pl_detail", "pl_head_id", "pl_head");
            DropIndex("pl_detail", new[] { "pl_head_id" });
            DropTable("pl_head");
            DropTable("pl_detail");
        }
    }
}
