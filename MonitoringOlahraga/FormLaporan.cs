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
    public partial class FormLaporan : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-MQ6MDQFG\\ARBYPANGESTU;Initial Catalog=DB_MonitoringOlahraga;Integrated Security=True";

        public FormLaporan()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void FormLaporan_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.CellClick += dataGridView1_CellClick;

            // ID diisi otomatis (IDENTITY), hanya tampil read-only
            txtIdLaporan.ReadOnly = true;
            txtIdLaporan.BackColor = System.Drawing.Color.LightGray;

            // Auto-load data saat form dibuka
            btnLoad.PerformClick();
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
           
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"UPDATE Laporan 
                        SET id_user = @id_user, 
                            periode_awal = @periode_awal, 
                            periode_akhir = @periode_akhir, 
                            total_keseluruhan_kalori = @total_kalori 
                        WHERE id_laporan = @id_laporan";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id_laporan", txtIdLaporan.Text);
                cmd.Parameters.AddWithValue("@id_user", txtIdUser.Text);
                cmd.Parameters.AddWithValue("@periode_awal", dtpAwal.Value.Date);
                cmd.Parameters.AddWithValue("@periode_akhir", dtpAkhir.Value.Date);
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
                    string query = "DELETE FROM Laporan WHERE id_laporan = @id_laporan";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_laporan", txtIdLaporan.Text);

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

                txtIdLaporan.Text = row.Cells["id_laporan"].Value.ToString();
                txtIdUser.Text = row.Cells["id_user"].Value.ToString();
                dtpAwal.Value = Convert.ToDateTime(row.Cells["periode_awal"].Value);
                dtpAkhir.Value = Convert.ToDateTime(row.Cells["periode_akhir"].Value);
                txtTotalKalori.Text = row.Cells["total_keseluruhan_kalori"].Value.ToString();
            }
        }

        private void ClearForm()
        {
            txtIdLaporan.Clear();
            txtIdUser.Clear();
            dtpAwal.Value = DateTime.Now;
            dtpAkhir.Value = DateTime.Now;
            txtTotalKalori.Clear();
            txtIdUser.Focus();
        }
    }
}
