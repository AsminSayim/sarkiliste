using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace SarkiListe
{
    public partial class FrmMain1 : Form
    {
        public FrmMain1()
        {
            InitializeComponent();
        }


        string baglanti = "Server=localhost;Database=muzik;Uid=root;Pwd=''";



        void VeriGetir()
        {
            using (MySqlConnection conn = new MySqlConnection(baglanti))
            {
                string sql = "SELECT *FROM sarkilar";
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                dgvListe.DataSource = dt;
            }
        }

        void TürleriGetir()
        {
            using (MySqlConnection conn = new MySqlConnection(baglanti))
            {
                string sql = "SELECT DISTINCT tur FROM sarkilar";
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                cmbTur.DataSource = dt;

                cmbTur.DisplayMember = "tur";
                cmbTur.ValueMember = "tur";
            }
        }


        private void FrmMain1_Load(object sender, EventArgs e)
        {
           
            VeriGetir();
            TürleriGetir();
        }

        private void dgvListe_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dgvListe.CurrentRow.Cells["id"].Value.ToString();
            txtSarkiAd.Text = dgvListe.CurrentRow.Cells["ad"].Value.ToString();
            txtSure.Text = dgvListe.CurrentRow.Cells["sure"].Value.ToString();
            txtYil.Text = dgvListe.CurrentRow.Cells["yil"].Value.ToString();
            txtSanatci.Text = dgvListe.CurrentRow.Cells["sanatci"].Value.ToString();
            cmbTur.Text = dgvListe.CurrentRow.Cells["tur"].Value.ToString();
            chkFavori.Checked = Convert.ToBoolean(dgvListe.CurrentRow.Cells["favori"].Value);
            dtpEklenmeTarihi.Value = Convert.ToDateTime(dgvListe.CurrentRow.Cells["eklenme_tarihi"].Value);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(baglanti))
            {
               string sql = "DELETE FROM sarkilar WHERE id=@id";
               int secilenId = Convert.ToInt32(txtId.Text);
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql,con);
                
               cmd.Parameters.AddWithValue("@id", secilenId);

               DialogResult result = MessageBox.Show("Kayıt silinsin mi?", "Kayıt Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
               {
                   cmd.ExecuteNonQuery();
                    VeriGetir();
               }

                 
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(baglanti))
            {
                string sql = "UPDATE sarkilar SET ad=@ad,sanatci=@sanatci,yil=@yil,tur=@tur,sure=@sure,eklenme_tarihi=@eklenme_tarihi,favori=@favori WHERE id=@id";
                int secilenId = Convert.ToInt32(txtId.Text);
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql, con);

                
                cmd.Parameters.AddWithValue("@ad", txtSarkiAd.Text);
                cmd.Parameters.AddWithValue("@sanatci", txtSanatci.Text);
                cmd.Parameters.AddWithValue("@yil", txtYil.Text);
                cmd.Parameters.AddWithValue("@tur", cmbTur.Text);
                cmd.Parameters.AddWithValue("@sure", txtSure.Text);
                cmd.Parameters.AddWithValue("@eklenme_tarihi", dtpEklenmeTarihi.Value);
                cmd.Parameters.AddWithValue("@favori",chkFavori.Checked);
                cmd.Parameters.AddWithValue("@id",secilenId);

                DialogResult result = MessageBox.Show("Kayıt güncellensin mi?", "Kayıt Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    VeriGetir();
                }


            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            


            using (MySqlConnection con = new MySqlConnection(baglanti))
            {

                string sql = "INSERT INTO sarkilar (ad,sure, yil, sanatci, tur, favori, eklenme_tarihi) " +
                             "VALUES (@ad, @sure, @yil, @sanatci, @tur, @favori, @eklenmeTarihi)";
                int secilenId = Convert.ToInt32(txtId.Text);

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@ad", txtSarkiAd.Text);
                cmd.Parameters.AddWithValue("@sanatci", txtSanatci.Text);
                cmd.Parameters.AddWithValue("@yil", txtYil.Text);
                cmd.Parameters.AddWithValue("@tur", cmbTur.Text);
                cmd.Parameters.AddWithValue("@sure", txtSure.Text);
                cmd.Parameters.AddWithValue("@eklenme_tarihi", dtpEklenmeTarihi.Value);
                cmd.Parameters.AddWithValue("@favori", chkFavori.Checked);
                cmd.Parameters.AddWithValue("@id", secilenId);


                DialogResult result = MessageBox.Show("Kayıt eklensin mi?", "Kayıt Ekle", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    VeriGetir();
                }


            }
        }

       
    }
    
}
