namespace Odev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoriler",
                c => new
                    {
                        KategoriId = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(),
                        KdvOrani = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.KategoriId);
            
            CreateTable(
                "dbo.Ürünler",
                c => new
                    {
                        UrunId = c.Int(nullable: false, identity: true),
                        UrunAdi = c.String(nullable: false, maxLength: 50),
                        stok = c.Int(nullable: false),
                        AlisFiyati = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SatisFiyati = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fotograf = c.Binary(),
                        BarkodNo = c.Int(nullable: false),
                        EklenmeZamani = c.DateTime(nullable: false),
                        KategoriId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UrunId)
                .ForeignKey("dbo.Kategoriler", t => t.KategoriId, cascadeDelete: true)
                .Index(t => t.KategoriId);
            
            CreateTable(
                "dbo.SiparisDetaylari",
                c => new
                    {
                        UrunId = c.Int(nullable: false),
                        SiparisId = c.Int(nullable: false),
                        Adet = c.Int(nullable: false),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Indirim = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.UrunId, t.SiparisId })
                .ForeignKey("dbo.Siparisler", t => t.SiparisId, cascadeDelete: true)
                .ForeignKey("dbo.Ürünler", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId)
                .Index(t => t.SiparisId);
            
            CreateTable(
                "dbo.Siparisler",
                c => new
                    {
                        SiparisId = c.Int(nullable: false, identity: true),
                        SiparisTarihi = c.DateTime(nullable: false),
                        TeslimTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.SiparisId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiparisDetaylari", "UrunId", "dbo.Ürünler");
            DropForeignKey("dbo.SiparisDetaylari", "SiparisId", "dbo.Siparisler");
            DropForeignKey("dbo.Ürünler", "KategoriId", "dbo.Kategoriler");
            DropIndex("dbo.SiparisDetaylari", new[] { "SiparisId" });
            DropIndex("dbo.SiparisDetaylari", new[] { "UrunId" });
            DropIndex("dbo.Ürünler", new[] { "KategoriId" });
            DropTable("dbo.Siparisler");
            DropTable("dbo.SiparisDetaylari");
            DropTable("dbo.Ürünler");
            DropTable("dbo.Kategoriler");
        }
    }
}
