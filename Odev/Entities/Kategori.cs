using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev.Entities
{
    [Table("Kategoriler")]
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        [Required]
        [StringLength(50)]
        public string KategoriAdi { get; set; }
        public string Aciklama { get; set; }
        public decimal KdvOrani { get; set; }

        public virtual List<Urun> Urunler { get; set; } = new List<Urun>();
    }
}
