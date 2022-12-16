using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Suyun_Şirinleri_
{
    public partial class satisPaneli : Form
    {
        public satisPaneli()
        {
            InitializeComponent();
        }

        private SqlConnection cnn = new SqlConnection("Data Source=LAPTOP-TO0C5CS2;Initial Catalog=suyun_sirinleriDB;Integrated Security=True");
        private SqlCommand komut;
        private SqlDataReader dr;

        public string[] renkler = { "", "Siyah-Beyaz", "Kahverengi", "Siyah-kırmızı", "Rengarenk" };
        public int veri, veriBarkod;
        private int eAdet, yAdet;
        public int barkodNo = 0;
        string resim;

        private void guncelle()
        {
            int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand komut = new SqlCommand("UPDATE urunler SET UrunAdeti =@adet WHERE Id = " + id, cnn);
            try
            {
                int gAdet = Convert.ToInt32(adetTxt.Text);
                yAdet = eAdet - gAdet;
                komut.Parameters.AddWithValue("@adet", Convert.ToInt32(yAdet));
                komut.ExecuteNonQuery();
                cnn.Close();

                adTxt.Text = "";
                cinsTxt.Text = "";
                fiyatTxt.Text = "";
                rengTxt.Text = "";
                adetTxt.Text = "";
            }
            catch (Exception a)
            {
            }
        }

        private void satBtn_Click(object sender, EventArgs e)
        {
            arama();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            int gAdet = Convert.ToInt32(adetTxt.Text);
            if (veriBarkod == barkodNo)
            {
                gAdet = gAdet + veri;
                komut.CommandText = "UPDATE satis_tablo SET UrunAdeti = @satisMiktari WHERE ID = " + barkodNo;
                komut.Parameters.AddWithValue("@satisMiktari", Convert.ToInt32(gAdet));
                komut.ExecuteNonQuery();
            }
            else
            {
                komut = new SqlCommand();
                try
                {
                    komut.CommandText = "INSERT INTO satis_tablo(ID, UrunAd, UrunCinsi , UrunFiyati , UrunRengi, satisTarihi , UrunAdeti) VALUES (@id, @urunAd, @cins , @fiyat , @renk , @satisTarihi , @adet)";
                    komut.Connection = cnn;
                    string urunAd = adTxt.Text, cins = cinsTxt.Text, fiyat = fiyatTxt.Text, renk = rengTxt.Text, girisTarih = satisTxt.Text, adet = adetTxt.Text;
                    komut.Parameters.AddWithValue("@id", barkodNo);
                    komut.Parameters.AddWithValue("@urunAd", urunAd);
                    komut.Parameters.AddWithValue("@cins", cins);
                    komut.Parameters.AddWithValue("@fiyat", Convert.ToDouble(fiyat));
                    komut.Parameters.AddWithValue("@renk", renk);
                    komut.Parameters.AddWithValue("@satisTarihi", Convert.ToDateTime(girisTarih));
                    komut.Parameters.AddWithValue("@adet", adet);
                    komut.ExecuteNonQuery();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message, "Envanter takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            cnn.Close();
            guncelle();
            adetSifir();
            stoklistele();
            satımlistele();
        }

        void resimSecme()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Id, UrunResim FROM urunler", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            string id = listView1.SelectedItems[0].Text;
            while (dr.Read())
            {
                if (id == dr["Id"].ToString())
                {
                    pictureBox2.ImageLocation = dr["UrunResim"].ToString();
                }
            }
            cnn.Close();
            dr.Close();
        }
        public void arama()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            komut = new SqlCommand("SELECT ID,UrunAdeti FROM satis_tablo", cnn);
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToInt32(dr["ID"]) == barkodNo)
                {
                    veriBarkod = Convert.ToInt32(dr["ID"]);
                    veri = Convert.ToInt32(dr["UrunAdeti"]);
                    break;
                }
            }
            cnn.Close();
        }

        private void stoklistele()
        {
            listView1.Items.Clear();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            komut = new SqlCommand("SELECT * FROM urunler", cnn);
            dr = komut.ExecuteReader();
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
            cnn.Close();
        }

        private void satisPaneli_Load(object sender, EventArgs e)
        {
            stoklistele();
            satımlistele();

            rengTxt.Items.AddRange(renkler);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in listView1.SelectedItems)
            {
                barkodNo = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                eAdet = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                adTxt.Text = listView1.SelectedItems[0].SubItems[1].Text;
                cinsTxt.Text = listView1.SelectedItems[0].SubItems[2].Text;
                fiyatTxt.Text = listView1.SelectedItems[0].SubItems[3].Text;
                rengTxt.Text = listView1.SelectedItems[0].SubItems[4].Text;
                adetTxt.Text = listView1.SelectedItems[0].SubItems[6].Text;
                resimSecme();
            }

        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog.FileName;
            resim = openFileDialog.FileName;
            MessageBox.Show(resim);
        }

        private void satımlistele()
        {
            listView2.Items.Clear();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            komut = new SqlCommand("SELECT * FROM satis_tablo", cnn);
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lw = new ListViewItem(dr["Id"].ToString());
                lw.SubItems.Add(dr["UrunAd"].ToString());
                lw.SubItems.Add(dr["UrunCinsi"].ToString());
                lw.SubItems.Add(dr["UrunFiyati"].ToString());
                lw.SubItems.Add(dr["UrunRengi"].ToString());
                lw.SubItems.Add(dr["satisTarihi"].ToString());
                lw.SubItems.Add(dr["UrunAdeti"].ToString());
                listView2.Items.Add(lw);
            }
            cnn.Close();

            toplamSatis();
        }

        private void adetSifir()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            if (yAdet == 0)
            {
                DialogResult secenek = MessageBox.Show("Adet sayınız 0'a ulaştı ürünü silmek ister misiniz?", "bilgilendirme penceresi", MessageBoxButtons.YesNo);
                if (secenek == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
                    komut.CommandText = "DELETE FROM urunler WHERE ID=" + id;
                    MessageBox.Show("silindi");
                    komut.ExecuteNonQuery();
                }
                else if (secenek == DialogResult.No)
                {
                    MessageBox.Show("Yakın Zamanda Tedarik ediniz.");
                }
            }
            cnn.Close();
        }

        private void toplamSatis()
        {
            int toplam = 0;
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            komut = new SqlCommand("SELECT UrunFiyati,UrunAdeti FROM satis_tablo", cnn);
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
               toplam += Convert.ToInt32(dr["UrunFiyati"]) * Convert.ToInt32(dr["UrunAdeti"]);
            }
            cnn.Close();
            label7.Text = "Toplam Satış Tutarı : " + toplam.ToString();
        }
    }
}