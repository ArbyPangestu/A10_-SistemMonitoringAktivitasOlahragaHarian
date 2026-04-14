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
    public partial class FormAktivitasOlahraga : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-MQ6MDQFG\\ARBYPANGESTU;Initial Catalog=DB_MonitoringOlahraga;Integrated Security=True";

        public FormAktivitasOlahraga()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void FormAktivitasOlahraga_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.CellClick += dataGridView1_CellClick;

            // ID diisi otomatis (IDENTITY), hanya tampil read-only
            txtIdAktivitas.ReadOnly = true;
            txtIdAktivitas.BackColor = System.Drawing.Color.LightGray;

            // Auto-load data saat form dibuka
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

                dataGridView1.Columns.Add("id_aktivitas", "ID Aktivitas");
                dataGridView1.Columns.Add("id_user", "ID User");
                dataGridView1.Columns.Add("id_jenis", "ID Jenis");
                dataGridView1.Columns.Add("durasi_menit", "Durasi (Menit)");
                dataGridView1.Columns.Add("tanggal", "Tanggal");
                dataGridView1.Columns.Add("total_kalori", "Total Kalori");

                string query = "SELECT * FROM AktivitasOlahraga";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["id_aktivitas"].ToString(),
                        reader["id_user"].ToString(),
                        reader["id_jenis"].ToString(),
                        reader["durasi_menit"].ToString(),
                        Convert.ToDateTime(reader["tanggal"]).ToShortDateString(),
                        reader["total_kalori"].ToString()
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

                if (string.IsNullOrEmpty(txtIdUser.Text))
                {
                    MessageBox.Show("ID User harus diisi");
                    txtIdUser.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtIdJenis.Text))
                {
                    MessageBox.Show("ID Jenis harus diisi");
                    txtIdJenis.Focus();
                    return;
                }

                // id_aktivitas adalah IDENTITY, tidak perlu diisi manual
                string query = @"INSERT INTO AktivitasOlahraga 
                                (id_user, id_jenis, durasi_menit, tanggal, total_kalori) 
                                VALUES 
                                (@id_user, @id_jenis, @durasi_menit, @tanggal, @total_kalori)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_user", txtIdUser.Text);
                cmd.Parameters.AddWithValue("@id_jenis", txtIdJenis.Text);
                cmd.Parameters.AddWithValue("@durasi_menit", txtDurasi.Text);
                cmd.Parameters.AddWithValue("@tanggal", dtpTanggal.Value.Date);
                cmd.Parameters.AddWithValue("@total_kalori", txtTotalKalori.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data aktivitas olahraga berhasil ditambahkan");
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

                string query = @"UPDATE AktivitasOlahraga 
                        SET id_user = @id_user, 
                            id_jenis = @id_jenis, 
                            durasi_menit = @durasi_menit, 
                            tanggal = @tanggal, 
                            total_kalori = @total_kalori 
                        WHERE id_aktivitas = @id_aktivitas";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id_aktivitas", txtIdAktivitas.Text);
                cmd.Parameters.AddWithValue("@id_user", txtIdUser.Text);
                cmd.Parameters.AddWithValue("@id_jenis", txtIdJenis.Text);
                cmd.Parameters.AddWithValue("@durasi_menit", txtDurasi.Text);
                cmd.Parameters.AddWithValue("@tanggal", dtpTanggal.Value.Date);
                cmd.Parameters.AddWithValue("@total_kalori", txtTotalKalori.Text);

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
                    string query = "DELETE FROM AktivitasOlahraga WHERE id_aktivitas = @id_aktivitas";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_aktivitas", txtIdAktivitas.Text);

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

                txtIdAktivitas.Text = row.Cells["id_aktivitas"].Value.ToString();
                txtIdUser.Text = row.Cells["id_user"].Value.ToString();
                txtIdJenis.Text = row.Cells["id_jenis"].Value.ToString();
                txtDurasi.Text = row.Cells["durasi_menit"].Value.ToString();
                dtpTanggal.Value = Convert.ToDateTime(row.Cells["tanggal"].Value);
                txtTotalKalori.Text = row.Cells["total_kalori"].Value.ToString();
            }
        }

        private void ClearForm()
        {
            txtIdAktivitas.Clear();
            txtIdUser.Clear();
            txtIdJenis.Clear();
            txtDurasi.Clear();
            dtpTanggal.Value = DateTime.Now;
            txtTotalKalori.Clear();
            txtIdUser.Focus();
        }
    }
}
