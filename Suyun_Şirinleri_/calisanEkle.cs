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
    public partial class calisanEkle : Form
    {
        public calisanEkle()
        {
            InitializeComponent();
        }
        string connetionString = "Data Source=LAPTOP-TO0C5CS2;Initial Catalog=suyun_sirinleriDB;Integrated Security=True";
        SqlConnection cnn = null;

        string[] roller = { "Yönetici", "Çalışan" };

        void listele()
        {
            listView1.Items.Clear();
            cnn = new SqlConnection(connetionString);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            bool giris = false;
            SqlCommand cmd = new SqlCommand("SELECT * FROM per_tablo", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lw = new ListViewItem(dr["kullaniciID"].ToString());
                lw.SubItems.Add(dr["kullaniciAdi"].ToString());
                lw.SubItems.Add(dr["kullaniciSoyadi"].ToString());
                lw.SubItems.Add(dr["sifre"].ToString());
                lw.SubItems.Add(dr["telefon"].ToString());
                lw.SubItems.Add(dr["rol"].ToString() == "True" ? "Yönetici" : "Çalışan");
                lw.SubItems.Add(dr["mail"].ToString());
                lw.SubItems.Add(dr["personelMaas"].ToString());
                listView1.Items.Add(lw);
            }
        }
        private void calisanEkle_Load(object sender, EventArgs e)
        {
            listele();
            rolTxt.Items.AddRange(roller);
        }

        private void TemizleBtn_Click(object sender, EventArgs e)
        {
            adTxt.Text = "";
            soyadTxt.Text = "";
            sifretxt.Text = "";
            telefontxt.Text = "";
            rolTxt.Text = "";
            mailTxt.Text = "";
            maasTxt.Text = "";
        }

        private void ekleBtn_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connetionString);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO per_tablo (kullaniciAdi, kullaniciSoyadi, sifre, telefon, rol, mail, personelMaas) VALUES (@ad,@soyad,@sifre,@telefon,@rol,@mail,@maas)", cnn);
            try
            {
                if (adTxt.Text != "" && soyadTxt.Text != "" && sifretxt.Text != "" && telefontxt.Text != "" && rolTxt.Text != "" && mailTxt.Text != "" && maasTxt.Text != "")
                {
                    string ad = adTxt.Text, soyad = soyadTxt.Text, sifre = sifretxt.Text, telefon = telefontxt.Text, rol = rolTxt.Text, mail = mailTxt.Text, maas = maasTxt.Text;
                    komut.Parameters.AddWithValue("@ad", ad);
                    komut.Parameters.AddWithValue("@soyad", soyad);
                    komut.Parameters.AddWithValue("@sifre", sifre);
                    komut.Parameters.AddWithValue("@telefon", telefon);
                    komut.Parameters.AddWithValue("@rol", rol == "Yönetici" ? "True" : "False");
                    komut.Parameters.AddWithValue("@mail", mail);
                    komut.Parameters.AddWithValue("@maas", maas);
                    komut.ExecuteNonQuery();
                    cnn.Close();
                    listele();
                }
                else
                    MessageBox.Show("lütfen boş alan bırakmayınız");
                adTxt.Text = "";
                soyadTxt.Text = "";
                sifretxt.Text = "";
                telefontxt.Text = "";
                rolTxt.Text = "";
                mailTxt.Text = "";
                maasTxt.Text = "";
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Envanter takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
