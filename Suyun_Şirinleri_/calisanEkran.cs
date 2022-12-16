using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suyun_Şirinleri_
{
    public partial class calisanEkran : Form
    {
        public calisanEkran()
        {
            InitializeComponent();
        }

        private void urunEklebtn_Click(object sender, EventArgs e)
        {
            ürünPaneli urunEkran = new ürünPaneli();//açılacak form
            urunEkran.MdiParent = this;//bu formu parent olarak veriyoruz.
            urunEkran.Show(); //form 2 açılıyor.
            urunEkran.Dock = DockStyle.Fill;
            urunEkran.BringToFront();
        }

        private void urunSatısBtn_Click(object sender, EventArgs e)
        {
            satisPaneli sp = new satisPaneli();//açılacak form
            sp.MdiParent = this;//bu formu parent olarak veriyoruz.
            sp.Show(); //form 2 açılıyor.
            sp.Dock = DockStyle.Fill;
            sp.BringToFront();
        }

        private void urunGuncelleBtn_Click(object sender, EventArgs e)
        {
            uGuncelle urunGuncelleme = new uGuncelle();//açılacak form
            urunGuncelleme.MdiParent = this;//bu formu parent olarak veriyoruz.
            urunGuncelleme.Show(); //form 2 açılıyor.
            urunGuncelleme.Dock = DockStyle.Fill;
            urunGuncelleme.BringToFront();
        }

        private void kapatma_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void altaAlma_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            urunArama arama = new urunArama();//açılacak form
            arama.MdiParent = this;//bu formu parent olarak veriyoruz.
            arama.Show(); //form 2 açılıyor.
            arama.Dock = DockStyle.Fill;
            arama.BringToFront();
        }
    }
}
