namespace Odev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ürünler", "BarkodNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ürünler", "BarkodNo", c => c.Int(nullable: false));
        }
    }
}
