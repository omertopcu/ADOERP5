using Odev.DAL;
using Odev.Dialog;
using Odev.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odev
{


    public partial class FormSiparis : Form
    {

        public FormSiparis()
        {
            InitializeComponent();
            arttırToolStripMenuItem.Click += SepetYonetimi;
            azaltToolStripMenuItem.Click += SepetYonetimi;
            çıkartToolStripMenuItem.Click += SepetYonetimi;
            adetiGüncelleToolStripMenuItem.Click += SepetYonetimi;
        }    
        private void FormSiparis_Load(object sender, EventArgs e)
        {
            VerileriGetir();
           
         
        }

        void VerileriGetir()
        {
            try
            {
                MyContext db = new MyContext();
                cmbKategori.DataSource = db.Kategoriler.ToList();
                cmbKategori.DisplayMember = "KategoriAdi";
                cmbKategori.ValueMember = "KategoriId";

                lstUrun.DataSource = db.Urunler.OrderBy(x => x.UrunAdi).ToList();
                lstUrun.DisplayMember = "UrunAdi";
                lstUrun.ValueMember = "UrunId";

                //this.Text = $"Toplam ürün sayısı: {db.Urunler.Count()} Toplam ürün maliyeti: {db.Urunler.Sum(x => x.AlisFiyati * x.Stok):c2} - {DateTime.Now:dd MMMM yy dddd}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSiparisOlustur_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
        bool StokKontrol(UrunViewModel seciliUrun, short adet = 1)
        {
            bool varmi = false, cevap = false;
            sepetList.ForEach(x =>
            {
                if (x.UrunId == seciliUrun.UrunId)
                {
                    varmi = true;
                    if (adet == 1)
                    {
                        if (seciliUrun.Stok > x.Adet)
                            cevap = true;
                        else
                            cevap = false;
                    }
                    else if (adet > 1)
                    {
                        if (seciliUrun.Stok >= adet)
                            cevap = true;
                        else
                            cevap = false;
                    }

                }
            });

            if (!varmi)
            {
                if (seciliUrun.Stok > 0)
                    cevap = true;
                else
                    cevap = false;
            }
            return cevap;
        }

        List<UrunViewModel> urunList = new List<UrunViewModel>();
        List<SepetViewModel> sepetList = new List<SepetViewModel>();
        void SepetHesapla()
        {
            decimal toplam = 0, kdv = 0;
            toplam = sepetList.Sum(x => x.Toplam);
            kdv = toplam * 0.18m;
            nToplam.Value = toplam;
            nKdv.Value = kdv;
        }
        void SepetGuncelle()
        {
            lstSepet.Items.Clear();
            sepetList.ForEach(x => lstSepet.Items.Add(x));
            SepetHesapla();
        }
        private void SepetYonetimi(object sender, EventArgs e)
        {
            if (lstSepet.SelectedItem == null) return;
            var seciliUrun = lstSepet.SelectedItem as SepetViewModel;

            var menuItem = sender as ToolStripMenuItem;
            if (menuItem.Name == "azaltToolStripMenuItem")
            {
                if (seciliUrun.Adet >= 2)
                    seciliUrun.Adet--;
                else if (seciliUrun.Adet == 1)
                    sepetList.Remove(seciliUrun);
            }
            else if (menuItem.Name == "arttırToolStripMenuItem")
            {
                var urun = urunList.Where(x => x.UrunId == seciliUrun.UrunId).FirstOrDefault();
                if (!StokKontrol(urun))
                {
                    MessageBox.Show("Stokta olandan fazlasını sepete ekleyemezsiniz");
                    return;
                }
                seciliUrun.Adet++;
            }

            else if (menuItem.Name == "çıkartToolStripMenuItem")
                sepetList.Remove(seciliUrun);
            else if (menuItem.Name == "adetiGüncelleToolStripMenuItem")
            {
                DialogAdetGuncelle dialogAdetGuncelle = new DialogAdetGuncelle();
                dialogAdetGuncelle.nDeger.Value = seciliUrun.Adet;
                dialogAdetGuncelle.StartPosition = FormStartPosition.CenterParent;
                dialogAdetGuncelle.ShowDialog();
                short deger = Convert.ToInt16(dialogAdetGuncelle.nDeger.Value);
                if (deger > 0)
                {
                    var urun = urunList.Where(x => x.UrunId == seciliUrun.UrunId).FirstOrDefault();
                    if (!StokKontrol(urun, deger))
                    {
                        MessageBox.Show("Stokta olandan fazlasını sepete ekleyemezsiniz");
                        return;
                    }
                    seciliUrun.Adet = deger;
                }
                else
                    sepetList.Remove(seciliUrun);
            }
            SepetGuncelle();
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

