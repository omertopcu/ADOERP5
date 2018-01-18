using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev.Entities
{
    [Table("Ürünler")]
    public class Urun
    {
        [Key]
        public int UrunId { get; set; }
        [Required]
        [StringLength(50)]
        public string UrunAdi { get; set; }
        public int Stok { get; set; } = 0;
        public decimal AlisFiyati { get; set; }
        public decimal SatisFiyati { get; set; }
        public byte[] Fotograf { get; set; }
        [Index(IsUnique =true)]
        [StringLength(40)]
        public string BarkodNo { get; set; }
        public DateTime EklenmeZamani { get; set; } = DateTime.Now;

        public int KategoriId { get; set; }

        [ForeignKey("KategoriId")]
        public virtual Kategori Kategori { get; set; }

        public virtual List<SiparisDetay> SiparisDetaylari { get; set; } = new List<SiparisDetay>();
    }
}
