using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Data.OleDb;

namespace Suyun_Şirinleri_
{
    public partial class giris : Form
    {
        string connetionString = "Data Source=LAPTOP-TO0C5CS2;Initial Catalog=suyun_sirinleriDB;Integrated Security=True";
        SqlConnection cnn = null;

        public giris()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void giris_Load(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connetionString);
            adTxt.Text = "Nisasu";
            sifreTxt.Text = "2304";
        }
        
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            cnn.Open();
            bool giris = false;
            SqlCommand cmd = new SqlCommand("SELECT * FROM per_tablo", cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            Form ekran;
            while (reader.Read())
            {
                if (reader["kullaniciAdi"].ToString() == adTxt.Text && reader["sifre"].ToString() == sifreTxt.Text)
                {
                    if (reader["rol"].ToString() == "True")
                    {
                        MessageBox.Show("Yönetici giriş yaptı,Hoşgeldiniz.");
                        ekran = new yoneticiEkrani();
                        this.Hide();
                        ekran.Show();
                        
                    }
                    if (reader["rol"].ToString() == "False")
                    {
                        MessageBox.Show("Çalışan girişi yapıldı.", reader["rol"].ToString());
                        ekran = new calisanEkran();
                        this.Hide();
                        ekran.Show();
                    }
                    giris = true;
                    break;
                }
                else
                {
                    giris = false;
                }
            }
            if (!giris)
            {
                MessageBox.Show("Hatalı Giriş.");
            }
            cnn.Close();
        }

        private void lblSifremiUnuttum_Click(object sender, EventArgs e)
        {
            SifremiUnuttumEkranı sifremiUnuttum = new SifremiUnuttumEkranı();
            sifremiUnuttum.Show();
        }

        private void sifreTxt_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
