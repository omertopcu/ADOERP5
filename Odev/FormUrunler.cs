using Odev.DAL;
using Odev.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odev
{
    public partial class FormUrunler : Form
    {
        public FormUrunler()
        {
            InitializeComponent();
        }

        private void FormUrunler_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }

        private void VerileriGetir()
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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (resimPath != null && resimPath != "")
            {
                FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                resim = br.ReadBytes((int)fs.Length);
            }
            Urun urun = new Urun()
            {
                AlisFiyati = nAlısFiyat.Value,
                SatisFiyati = nSatisFiyat.Value,
                BarkodNo = txtBarkodNo.Text,
                UrunAdi = txtUrunAdi.Text,
                Stok = (int)nStok.Value,
                KategoriId = (int)cmbKategori.SelectedValue,
                Fotograf = resim
               
            };
            try
            {
                MyContext db = new MyContext();
                db.Urunler.Add(urun);
                db.SaveChanges();
                VerileriGetir();
                lstUrun.SelectedValue = urun.UrunId;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstUrun.SelectedItem == null) return;
            var seciliUrun = lstUrun.SelectedItem as Urun;
            var cevap = MessageBox.Show($"{seciliUrun.UrunAdi} adlı ürünü silmek istediğinize emin misiniz", "Ürün Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes) return;
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    MyContext db = new MyContext();
                    seciliUrun = db.Urunler.Find(seciliUrun.UrunId);
                    if (seciliUrun == null)
                    {
                        MessageBox.Show("Ürün bulunamadı");
                        VerileriGetir();
                        return;
                    }
                    db.Urunler.Remove(seciliUrun);
                    db.SaveChanges();
                    VerileriGetir();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (lstUrun.SelectedItem == null) return;
            var seciliUrun = lstUrun.SelectedItem as Urun;
            try
            {
                MyContext db = new MyContext();
                seciliUrun = db.Urunler.Find(seciliUrun.UrunId);
                if (seciliUrun == null)
                {

                    MessageBox.Show("Ürün Bulunamadı.");
                    VerileriGetir();

                }
                seciliUrun.UrunAdi = txtUrunAdi.Text;
                seciliUrun.AlisFiyati = nAlısFiyat.Value;
                seciliUrun.SatisFiyati = nSatisFiyat.Value;
                seciliUrun.Stok = Convert.ToInt16(nStok.Value);
                seciliUrun.KategoriId = (int)cmbKategori.SelectedValue;
                seciliUrun.BarkodNo = txtBarkodNo.Text;               
                lstUrun.SelectedValue = seciliUrun.UrunId;
                if (txtResim.Text == string.Empty)
                {
                    resim = seciliUrun.Fotograf;
                }
                else
                {                  
                    FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    resim = br.ReadBytes((int)fs.Length);
                }
                seciliUrun.Fotograf = resim;   
                db.SaveChanges();
                VerileriGetir();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void pctResim_Click(object sender, EventArgs e)
        {

        }

        private void lstUrun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUrun.SelectedItem == null) return;

            var seciliUrun = lstUrun.SelectedItem as Urun;
            txtUrunAdi.Text = seciliUrun.UrunAdi;
            nAlısFiyat.Value = seciliUrun.AlisFiyati;
            nSatisFiyat.Value = seciliUrun.SatisFiyati;
            cmbKategori.SelectedValue = seciliUrun.KategoriId;
            nStok.Value = seciliUrun.Stok;
            txtBarkodNo.Text = seciliUrun.BarkodNo;
            byte[] emptyArray = new byte[0];
            if (seciliUrun.Fotograf != null)
            {
                var ms = new MemoryStream(seciliUrun.Fotograf ?? emptyArray);
                var ResimGoster = Image.FromStream(ms);
                pctResim.Image = ResimGoster;
            }
            else
            {
                pctResim.Image = null;
            }
        }
        byte[] resim;
        string resimPath;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Aç";
            openFileDialog1.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pctResim.Image = Image.FromFile(openFileDialog1.FileName);

                txtResim.Text = openFileDialog1.FileName.ToString();
                resimPath = openFileDialog1.FileName.ToString();
            }
        }
    }
}
