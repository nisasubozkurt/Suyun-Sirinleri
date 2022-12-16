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
    public partial class yoneticiEkrani : Form
    {
        public yoneticiEkrani()
        {
            InitializeComponent();
            this.IsMdiContainer = true; //Form içinde form açılabilir yapıyoruz
        }

        private void urunEklebtn_Click(object sender, EventArgs e)
        {
            ürünPaneli urunEkran = new ürünPaneli();//açılacak form
            urunEkran.MdiParent = this;//bu formu parent olarak veriyoruz.
            urunEkran.Show(); //form 2 açılıyor.
            urunEkran.Dock = DockStyle.Fill;
            urunEkran.BringToFront();
        }

        private void calisanEkleBtn_Click(object sender, EventArgs e)
        {
            calisanEkle calisanekle = new calisanEkle ();//açılacak form
            calisanekle.MdiParent = this;//bu formu parent olarak veriyoruz.
            calisanekle.Show(); //form 2 açılıyor.
            calisanekle.Dock = DockStyle.Fill;
            calisanekle.BringToFront();
        }

        private void altaAlma_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void kapatma_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void yoneticiEkrani_Load(object sender, EventArgs e)
        {

        }

        private void calisanGuncelleBtn_Click(object sender, EventArgs e)
        {
            cGuncelle calisanGuncelleme = new cGuncelle();//açılacak form
            calisanGuncelleme.MdiParent = this;//bu formu parent olarak veriyoruz.
            calisanGuncelleme.Show(); //form 2 açılıyor.
            calisanGuncelleme.Dock = DockStyle.Fill;
            calisanGuncelleme.BringToFront();
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
