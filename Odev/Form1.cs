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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ürünlerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
       
        private void ürünOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUrunler urunlerForm = new FormUrunler
            {
                Text = "Ürünler"
            };
            urunlerForm.ShowDialog();      
        }

        private void kategoriOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKategoriler kategorilerForm = new FormKategoriler
            {
                Text = "Kategoriler"
            };
            kategorilerForm.ShowDialog();
        }

        private void siparişOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormSiparis formSiparis = new FormSiparis
            {
                Text = "Siparişler"
            };
            formSiparis.ShowDialog();


        }
    }
}
