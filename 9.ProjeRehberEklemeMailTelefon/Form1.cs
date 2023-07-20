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

namespace _9.ProjeRehberEklemeMailTelefon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-1DQCP20\SQLEXPRESS;Initial Catalog=9.ProjeRehber;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter dr = new SqlDataAdapter("select * from KISILER", baglanti);
            DataTable DT = new DataTable();
            dr.Fill(DT);
            dataGridView1.DataSource = DT;
        }
        void temizle() 
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtTelefon.Text = "";
            TxtMail.Text = "";
            TxtId.Text = "";
            TxtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
     
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand KOMUT1 = new SqlCommand("insert into kısıler (AD, SOYAD, TELEFON, MAIL) values(@P1, @P2, @P3, @P4)",baglanti);
            KOMUT1.Parameters.AddWithValue("@P1", TxtAd.Text);
            KOMUT1.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            KOMUT1.Parameters.AddWithValue("@P3", TxtTelefon.Text);
            KOMUT1.Parameters.AddWithValue("@P4", TxtMail.Text);
            KOMUT1.ExecuteNonQuery();
            MessageBox.Show("Successfully added");
            baglanti.Close();
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Update KISILER SET AD=@P1, SOYAD=@P2, TELEFON=@P3, MAIL=@P4 WHERE ID=@P5",baglanti);
            komut2.Parameters.AddWithValue("@p5",TxtId.Text);
            komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", TxtTelefon.Text);
            komut2.Parameters.AddWithValue("@p4", TxtMail.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Successfully updated ");
            listele();
            temizle();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtTelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();

                }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand KOMUT3 = new SqlCommand("Delete from kısıler where ID=@p1",baglanti);
            KOMUT3.Parameters.AddWithValue("@p1", TxtId.Text);
            KOMUT3.ExecuteNonQuery();
            MessageBox.Show("Successfully deleted");
            baglanti.Close();
            listele();
            temizle();
        }
    }
}
