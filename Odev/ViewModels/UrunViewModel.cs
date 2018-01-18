using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev.ViewModels
{
    public class UrunViewModel
    {
  
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Stok { get; set; } = 0;
        public decimal AlisFiyati { get; set; }
        public decimal SatisFiyati { get; set; }
        public string BarkodNo { get; set; }
        public DateTime EklenmeZamani { get; set; } = DateTime.Now;
        public string KategoriAdi { get; set; }
        public int KategoriId { get; set; }
     
        public override string ToString() => $"{KategoriAdi} {UrunAdi}: {SatisFiyati:c2} - Kalan: {Stok}";
    }
}
