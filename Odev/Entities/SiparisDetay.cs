using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev.Entities
{
    [Table("SiparisDetaylari")]
   public class SiparisDetay
    {
        [Key]
        [Column(Order = 1)]
        public int UrunId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int SiparisId { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Indirim { get; set; } = 0;

        [ForeignKey("SiparisId")]
        public virtual Siparis Siparis { get; set; }

        [ForeignKey("UrunId")]
        public virtual Urun urun { get; set; }

    }
}
