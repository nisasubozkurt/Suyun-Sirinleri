using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suyun_Şirinleri_
{
    public partial class uGuncelle : Form
    {
        string connetionString = "Data Source=.;Initial Catalog=suyun_sirinleriDB;Integrated Security=True";
        SqlConnection cnn = null;

        public string[] cinsler = { "", "Peluş Oyuncak", "Oyuncak Araba", "Oyuncak Bebek", "Ahşap Oyuncaklar", "Bebek Oyuncakları", "Eğitici Oyuncaklar", "Figür Oyuncaklar", "Hobi Oyuncakları", "Kutu Oyuncakları", "Puzzle" };
        public string[] renkler = { "", "Siyah", "Beyaz", "Kahverengi", "Kırmızı", "Çok Renkli", "Altın", "Bej", "Gri", "Lacivert", "Mavi", "Mor", "Pembe", "Sarı", "Turuncu", "Yeşil" };
        string resim;
        public uGuncelle()
        {
            InitializeComponent();
        }
        void listele()
        {
            listView1.Items.Clear();
            cnn = new SqlConnection(connetionString);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM urunler", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lw = new ListViewItem(dr["Id"].ToString());
                lw.SubItems.Add(dr["UrunAdi"].ToString());
                lw.SubItems.Add(dr["UrunCinsi"].ToString());
                lw.SubItems.Add(dr["UrunFiyati"].ToString());
                lw.SubItems.Add(dr["UrunRengi"].ToString());
                lw.SubItems.Add(dr["GirisTarihi"].ToString());
                lw.SubItems.Add(dr["UrunAdeti"].ToString());
                listView1.Items.Add(lw);
            }
        }
        private void uGuncelle_Load(object sender, EventArgs e)
        {
            listele();

            {
                cinsTxt.Items.AddRange(cinsler);
                rengTxt.Items.AddRange(renkler);
            }
        }

        private void guncelleBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
            cnn = new SqlConnection(connetionString);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand komut = new SqlCommand("UPDATE urunler SET UrunAdi = @urunAd, UrunCinsi = @cins, UrunFiyati = @fiyat, UrunRengi = @renk, UrunAdeti =@adet, UrunResim = @resim WHERE Id = " + id, cnn);
            try
            {
                if (adTxt.Text != "" && cinsTxt.Text != "" && fiyatTxt.Text != "" && rengTxt.Text != "" && adetTxt.Text != "")
                {
                    string urunAd = adTxt.Text, cins = cinsTxt.Text, fiyat = fiyatTxt.Text, renk = rengTxt.Text, adet = adetTxt.Text;
                    komut.Parameters.AddWithValue("@urunAd", urunAd);
                    komut.Parameters.AddWithValue("@cins", cins);
                    komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(fiyat));
                    komut.Parameters.AddWithValue("@renk", renk);
                    komut.Parameters.AddWithValue("@adet", adet);
                    komut.Parameters.AddWithValue("@resim", resim);
                    komut.ExecuteNonQuery();
                    cnn.Close();
                    listele();
                }
                else
                    MessageBox.Show("lütfen boş alan bırakmayınız");
                adTxt.Text = "";
                cinsTxt.Text = "";
                fiyatTxt.Text = "";
                rengTxt.Text = "";
                adetTxt.Text = "";
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Envanter takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in listView1.SelectedItems)
            {
                adTxt.Text = listView1.SelectedItems[0].SubItems[1].Text;
                cinsTxt.Text = listView1.SelectedItems[0].SubItems[2].Text;
                fiyatTxt.Text = listView1.SelectedItems[0].SubItems[3].Text;
                rengTxt.Text = listView1.SelectedItems[0].SubItems[4].Text;
                adetTxt.Text = listView1.SelectedItems[0].SubItems[6].Text;
                resimSecme();
            }
        }

        void resimSecme()
        {
            cnn = new SqlConnection(connetionString);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Id, UrunResim FROM urunler", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            string id = listView1.SelectedItems[0].Text;
            while (dr.Read())
            {
                if (id == dr["Id"].ToString())
                {
                    pictureBox2.ImageLocation =  dr["UrunResim"].ToString();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog.FileName;
            resim = openFileDialog.FileName;
        }
    }
}
