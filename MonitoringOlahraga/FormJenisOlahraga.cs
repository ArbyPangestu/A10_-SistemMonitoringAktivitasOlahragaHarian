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

namespace MonitoringOlahraga
{
    public partial class FormJenisOlahraga : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-MQ6MDQFG\\ARBYPANGESTU;Initial Catalog=DB_MonitoringOlahraga;Integrated Security=True";

        public FormJenisOlahraga()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void FormJenisOlahraga_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.CellClick += dataGridView1_CellClick;

 
            txtIdJenis.ReadOnly = true;
            txtIdJenis.BackColor = System.Drawing.Color.LightGray;


            btnLoad.PerformClick();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("id_jenis", "ID Jenis");
                dataGridView1.Columns.Add("nama_olahraga", "Nama Olahraga");
                dataGridView1.Columns.Add("kalori_per_menit", "Kalori / Menit");

                string query = "SELECT * FROM JenisOlahraga";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["id_jenis"].ToString(),
                        reader["nama_olahraga"].ToString(),
                        reader["kalori_per_menit"].ToString()
                    );
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (string.IsNullOrEmpty(txtNamaOlahraga.Text))
                {
                    MessageBox.Show("Nama Olahraga harus diisi");
                    txtNamaOlahraga.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtKalori.Text))
                {
                    MessageBox.Show("Kalori per menit harus diisi");
                    txtKalori.Focus();
                    return;
                }

                // id_jenis adalah IDENTITY, tidak perlu diisi manual
                string query = @"INSERT INTO JenisOlahraga 
                                (nama_olahraga, kalori_per_menit) 
                                VALUES 
                                (@nama_olahraga, @kalori_per_menit)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama_olahraga", txtNamaOlahraga.Text);
                cmd.Parameters.AddWithValue("@kalori_per_menit", txtKalori.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data jenis olahraga berhasil ditambahkan");
                    ClearForm();
                    btnLoad.PerformClick();
                }
                else
                {
                    MessageBox.Show("Data gagal ditambahkan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"UPDATE JenisOlahraga 
                        SET nama_olahraga = @nama_olahraga, 
                            kalori_per_menit = @kalori_per_menit 
                        WHERE id_jenis = @id_jenis";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id_jenis", txtIdJenis.Text);
                cmd.Parameters.AddWithValue("@nama_olahraga", txtNamaOlahraga.Text);
                cmd.Parameters.AddWithValue("@kalori_per_menit", txtKalori.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data berhasil diupdate");
                    ClearForm();
                    btnLoad.PerformClick();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DialogResult resultConfirm = MessageBox.Show(
                    "Yakin ingin menghapus data?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultConfirm == DialogResult.Yes)
                {
                    string query = "DELETE FROM JenisOlahraga WHERE id_jenis = @id_jenis";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_jenis", txtIdJenis.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus");
                        ClearForm();
                        btnLoad.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtIdJenis.Text = row.Cells["id_jenis"].Value.ToString();
                txtNamaOlahraga.Text = row.Cells["nama_olahraga"].Value.ToString();
                txtKalori.Text = row.Cells["kalori_per_menit"].Value.ToString();
            }
        }

        private void ClearForm()
        {
            txtIdJenis.Clear();
            txtNamaOlahraga.Clear();
            txtKalori.Clear();
            txtNamaOlahraga.Focus();
        }
    }
}
