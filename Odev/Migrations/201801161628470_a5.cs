namespace Odev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ürünler", "BarkodNo", c => c.String(maxLength: 40));
            CreateIndex("dbo.Ürünler", "BarkodNo", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ürünler", new[] { "BarkodNo" });
            AlterColumn("dbo.Ürünler", "BarkodNo", c => c.String());
        }
    }
}
