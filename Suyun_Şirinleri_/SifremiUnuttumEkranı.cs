using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suyun_Şirinleri_
{
    public partial class SifremiUnuttumEkranı : Form
    {
        public SifremiUnuttumEkranı()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            string sifre = null;
            try
            {
               // SqlConnection cnn = new SqlConnection("Data Source=LAPTOP-TO0C5CS2;Initial Catalog=suyun_sirinleriDB;Integrated Security=True");
                SqlConnection cnn = new SqlConnection("Data Source=.;Initial Catalog=suyun_sirinleriDB;Integrated Security=True");
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM per_tablo", cnn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["mail"].ToString() == textBox1.Text)
                    {
                        sifre = reader["sifre"].ToString();
                        break;
                    }
                }
                string mesaj = "Hesabınızın Şifresi: " + sifre;
                MailGonder("Hesabınızın Şifresi", mesaj);

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            
            this.Close();
        }

        public bool MailGonder(string konu, string icerik)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("suyunsirinleri@gmail.com");
            ePosta.To.Add(textBox1.Text); //göndereceğimiz mail adresi

            ePosta.Subject = konu; //mail konusu
            ePosta.Body = icerik; //mail içeriği 

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("suyunsirinleri@gmail.com", "dzmmhpvxoqhcpngv");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(ePosta);
            object userState = true;
            bool kontrol = true;
            try
            {
                client.SendAsync(ePosta, (object)ePosta);
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                MessageBox.Show(ex.Message);
            }
            return kontrol;
        }

        private void SifremiUnuttumEkranı_Load(object sender, EventArgs e)
        {

        }
    }
}
