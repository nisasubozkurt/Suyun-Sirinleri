using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Suyun_Şirinleri_
{
    public partial class cGuncelle : Form
    {
        public cGuncelle()
        {
            InitializeComponent();
        }

        private string connetionString = "Data Source=LAPTOP-TO0C5CS2;Initial Catalog=suyun_sirinleriDB;Integrated Security=True";
        private SqlConnection cnn = null;

        private string[] roller = { "Yönetici", "Çalışan" };

        private void listele()
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

        private void cGuncelle_Load(object sender, EventArgs e)
        {
            listele();
            rolTxt.Items.AddRange(roller);
        }

        private void guncelleBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
            cnn = new SqlConnection(connetionString);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            SqlCommand komut = new SqlCommand("UPDATE per_tablo SET kullaniciAdi = @ad, kullaniciSoyadi = @soyad, sifre = @sifre, telefon = @telefon, rol = @rol, mail = @mail, personelMaas = @maas WHERE kullaniciID = " + id, cnn);
            try
            {
                if (adTxt.Text != "" && soyadTxt.Text != "" && sifretxt.Text != "" && telefontxt.Text != "" && rolTxt.Text != "" && mailTxt.Text != "" && maasTxt.Text != "")
                {
                    string ad = adTxt.Text, soyad = soyadTxt.Text, sifre = sifretxt.Text, telefon = telefontxt.Text, rol = rolTxt.Text, mail = mailTxt.Text, maas = maasTxt.Text;
                    komut.Parameters.AddWithValue("@ad", ad);
                    komut.Parameters.AddWithValue("@soyad", soyad);
                    komut.Parameters.AddWithValue("@sifre", sifre);
                    komut.Parameters.AddWithValue("@telefon", telefon);
                    komut.Parameters.AddWithValue("@rol", rol=="Yönetici"? "True":"False");
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in listView1.SelectedItems)
            {
                adTxt.Text = listView1.SelectedItems[0].SubItems[1].Text;
                soyadTxt.Text = listView1.SelectedItems[0].SubItems[2].Text;
                sifretxt.Text = listView1.SelectedItems[0].SubItems[3].Text;
                telefontxt.Text = listView1.SelectedItems[0].SubItems[4].Text;
                rolTxt.Text = listView1.SelectedItems[0].SubItems[5].Text;
                mailTxt.Text = listView1.SelectedItems[0].SubItems[6].Text;
                maasTxt.Text = listView1.SelectedItems[0].SubItems[7].Text;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
            cnn = new SqlConnection(connetionString);
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            
            SqlCommand komut = new SqlCommand("DELETE FROM per_tablo WHERE kullaniciID=" + id, cnn);
            komut.ExecuteNonQuery();
            cnn.Close();
            listele();
            adTxt.Text = "";
            soyadTxt.Text = "";
            sifretxt.Text = "";
            telefontxt.Text = "";
            rolTxt.Text = "";
            mailTxt.Text = "";
            maasTxt.Text = "";
        }
    }
}