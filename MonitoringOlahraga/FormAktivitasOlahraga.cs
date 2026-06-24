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
using System.IO;
using ExcelDataReader;

namespace MonitoringOlahraga
{
    public partial class FormAktivitasOlahraga : Form
    {
        private int _selectedIdAktivitas = 0;
        private readonly SqlConnection conn;
        private readonly string connectionString = DatabaseHelper.GetConnectionString();
        
        private Button btnImportExcel;
        private Button btnSaveExcelToDB;
        private DataTable dtImported;

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

            btnImportExcel = new Button();
            btnImportExcel.Text = "Import Excel";
            btnImportExcel.Location = new Point(385, 5);
            btnImportExcel.Size = new Size(100, 28);
            btnImportExcel.BackColor = Color.FromArgb(40, 160, 160);
            btnImportExcel.ForeColor = Color.White;
            btnImportExcel.FlatStyle = FlatStyle.Flat;
            btnImportExcel.FlatAppearance.BorderSize = 0;
            btnImportExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnImportExcel.Click += btnImportExcel_Click;
            pnlButtons.Controls.Add(btnImportExcel);

            btnSaveExcelToDB = new Button();
            btnSaveExcelToDB.Text = "Save DB";
            btnSaveExcelToDB.Location = new Point(490, 5);
            btnSaveExcelToDB.Size = new Size(100, 28);
            btnSaveExcelToDB.BackColor = Color.FromArgb(60, 100, 160);
            btnSaveExcelToDB.ForeColor = Color.White;
            btnSaveExcelToDB.FlatStyle = FlatStyle.Flat;
            btnSaveExcelToDB.FlatAppearance.BorderSize = 0;
            btnSaveExcelToDB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSaveExcelToDB.Click += btnSaveExcelToDB_Click;
            pnlButtons.Controls.Add(btnSaveExcelToDB);

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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                dtImported = result.Tables[0];
                                dataGridView1.DataSource = dtImported;
                                MessageBox.Show("Data Excel berhasil di-load ke Grid.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading Excel: " + ex.Message);
                    }
                }
            }
        }

        private void btnSaveExcelToDB_Click(object sender, EventArgs e)
        {
            if (dtImported == null || dtImported.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data Excel untuk disimpan.");
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Validasi: minimal harus ada 5 kolom
                        if (dtImported.Columns.Count < 5)
                        {
                            MessageBox.Show("Format Excel tidak valid! Pastikan file Excel Anda memiliki 5 kolom:\nKolom 1: id_user\nKolom 2: nama_olahraga\nKolom 3: kalori_per_menit\nKolom 4: durasi_menit\nKolom 5: tanggal");
                            trans.Rollback();
                            return;
                        }

                        int berhasil = 0;
                        foreach (DataRow row in dtImported.Rows)
                        {
                            // Akses dengan INDEX kolom (0,1,2,3,4) agar tidak bergantung pada nama header
                            // Kolom 0: id_user, Kolom 1: nama_olahraga, Kolom 2: kalori_per_menit, Kolom 3: durasi_menit, Kolom 4: tanggal
                            try
                            {
                                object valIdUser = row[0];
                                object valNama = row[1];
                                object valKalori = row[2];
                                object valDurasi = row[3];
                                object valTanggal = row[4];

                                // Skip baris kosong
                                if (valNama == null || valNama == DBNull.Value || string.IsNullOrWhiteSpace(valNama.ToString()))
                                    continue;

                                SqlCommand cmd = new SqlCommand("sp_InsertAktivitas", conn, trans);
                                cmd.CommandType = CommandType.StoredProcedure;
                                
                                cmd.Parameters.AddWithValue("@id_user", (valIdUser != null && valIdUser != DBNull.Value && !string.IsNullOrWhiteSpace(valIdUser.ToString())) ? Convert.ToInt32(valIdUser) : 1);
                                cmd.Parameters.AddWithValue("@nama_olahraga", valNama.ToString().Trim());
                                cmd.Parameters.AddWithValue("@kalori_per_menit", Convert.ToInt32(valKalori));
                                cmd.Parameters.AddWithValue("@durasi_menit", Convert.ToInt32(valDurasi));
                                cmd.Parameters.AddWithValue("@tanggal", Convert.ToDateTime(valTanggal));

                                cmd.ExecuteNonQuery();
                                berhasil++;
                            }
                            catch (Exception rowEx)
                            {
                                // Skip baris yang bermasalah (misal baris header yang terbaca sebagai data)
                                System.Diagnostics.Debug.WriteLine("Skip baris: " + rowEx.Message);
                                continue;
                            }
                        }
                        
                        trans.Commit();
                        MessageBox.Show($"Berhasil! {berhasil} baris data Excel telah disimpan ke Database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtImported = null;
                        btnLoad.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Terjadi kesalahan saat menyimpan data (Transaction Rolled Back): " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
