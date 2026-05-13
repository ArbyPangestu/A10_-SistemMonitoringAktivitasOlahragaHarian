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
        private int _selectedIdAktivitas = 0;
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


            btnLoad.PerformClick();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM vw_RiwayatAktivitas";
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;

                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;

                // Sembunyikan kolom ID secara aman
                if (dataGridView1.Columns["id_aktivitas"] != null) dataGridView1.Columns["id_aktivitas"].Visible = false;
                if (dataGridView1.Columns["id_user"] != null) dataGridView1.Columns["id_user"].Visible = false;
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
                if (conn.State == ConnectionState.Closed) conn.Open();

                if (string.IsNullOrEmpty(txtNamaOlahraga.Text))
                {
                    MessageBox.Show("Nama Olahraga harus diisi");
                    txtNamaOlahraga.Focus();
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_InsertAktivitas", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_user", 1);
                cmd.Parameters.AddWithValue("@nama_olahraga", txtNamaOlahraga.Text);
                cmd.Parameters.AddWithValue("@kalori_per_menit", txtKaloriPerMenit.Text);
                cmd.Parameters.AddWithValue("@durasi_menit", txtDurasi.Text);
                cmd.Parameters.AddWithValue("@tanggal", dtpTanggal.Value.Date);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data aktivitas olahraga berhasil ditambahkan");
                    ClearForm();
                    btnLoad.PerformClick();
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
                if (conn.State == ConnectionState.Closed) conn.Open();

                if (_selectedIdAktivitas == 0)
                {
                    MessageBox.Show("Pilih data dari tabel terlebih dahulu.");
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_UpdateAktivitas", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_aktivitas", _selectedIdAktivitas);
                cmd.Parameters.AddWithValue("@nama_olahraga", txtNamaOlahraga.Text);
                cmd.Parameters.AddWithValue("@kalori_per_menit", txtKaloriPerMenit.Text);
                cmd.Parameters.AddWithValue("@durasi_menit", txtDurasi.Text);
                cmd.Parameters.AddWithValue("@tanggal", dtpTanggal.Value.Date);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data berhasil diupdate");
                    ClearForm();
                    btnLoad.PerformClick();
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
                if (conn.State == ConnectionState.Closed) conn.Open();

                if (_selectedIdAktivitas == 0)
                {
                    MessageBox.Show("Pilih data dari tabel terlebih dahulu.");
                    return;
                }

                if (MessageBox.Show("Yakin ingin menghapus data?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteAktivitas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_aktivitas", _selectedIdAktivitas);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus");
                        ClearForm();
                        btnLoad.PerformClick();
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

                _selectedIdAktivitas = Convert.ToInt32(row.Cells["id_aktivitas"].Value);
                txtNamaOlahraga.Text = row.Cells["nama_olahraga"].Value.ToString();
                txtKaloriPerMenit.Text = row.Cells["kalori_per_menit"].Value.ToString();
                txtDurasi.Text = row.Cells["durasi_menit"].Value.ToString();
                dtpTanggal.Value = Convert.ToDateTime(row.Cells["tanggal"].Value);
            }
        }

        private void ClearForm()
        {
            _selectedIdAktivitas = 0;
            txtNamaOlahraga.Clear();
            txtKaloriPerMenit.Clear();
            txtDurasi.Clear();
            dtpTanggal.Value = DateTime.Now;
            txtNamaOlahraga.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter("sp_SearchAktivitas", conn))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@keyword", txtSearch.Text);
                    da.Fill(dt);
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pencarian gagal: " + ex.Message);
            }
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
