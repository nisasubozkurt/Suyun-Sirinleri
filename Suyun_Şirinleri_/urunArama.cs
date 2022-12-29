using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Suyun_Şirinleri_
{
    public partial class urunArama : Form
    {
        public urunArama()
        {
            InitializeComponent();
        }

        private SqlConnection cnn = new SqlConnection("Data Source=.;Initial Catalog=suyun_sirinleriDB;Integrated Security=True");
        private SqlCommand komut;
        private SqlDataReader dr;

        private void satBtn_Click(object sender, EventArgs e)
        {
            if (stokTBL.Checked)
            {
                stokGroup.Visible = true;
                satisGroup.Visible = false;
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                komut = new SqlCommand("SELECT * FROM urunler", cnn);
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["UrunAdi"].ToString() == adTxt.Text)
                    {
                        stokAd.Text = dr["UrunAdi"].ToString();
                        stokCins.Text = dr["UrunCinsi"].ToString();
                        stokFiyat.Text = dr["UrunFiyati"].ToString();
                        stokRenk.Text = dr["UrunRengi"].ToString();
                        stokAdet.Text = dr["UrunAdeti"].ToString();
                    }
                }
                cnn.Close();
                dr.Close();
                resimArama();
            }
            else
            {
                stokGroup.Visible = false;
                satisGroup.Visible = true;
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                komut = new SqlCommand("SELECT * FROM satis_tablo", cnn);
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["UrunAd"].ToString() == adTxt.Text)
                    {
                        satilmisAd.Text = dr["UrunAd"].ToString();
                        satilmisCins.Text = dr["UrunCinsi"].ToString();
                        satilmisFiyat.Text = dr["UrunFiyati"].ToString();
                        satilmisRenk.Text = dr["UrunRengi"].ToString();
                        satilmisAdet.Text = dr["UrunAdeti"].ToString();
                    }
                }
                cnn.Close();
                dr.Close();
                resimArama();
            }
        }
        void resimArama()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            komut = new SqlCommand("SELECT * FROM urunler", cnn);
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                if (dr["UrunAdi"].ToString() == adTxt.Text)
                {
                    pictureBox1.ImageLocation = dr["UrunResim"].ToString();
                }
            }
            cnn.Close();
            dr.Close();
        }

        private void urunArama_Load(object sender, EventArgs e)
        {
            stokTBL.Checked = true;
        }
    }
}