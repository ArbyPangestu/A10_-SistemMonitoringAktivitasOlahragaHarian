using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
// using CrystalDecisions.CrystalReports.Engine;

namespace MonitoringOlahraga
{
    public partial class FormRekap : Form
    {
        private SqlConnection conn;

        public FormRekap()
        {
            InitializeComponent();
            conn = new SqlConnection(DatabaseHelper.GetConnectionString());
        }

        private void FormRekap_Load(object sender, EventArgs e)
        {
            LoadUserComboBox();
        }

        private void LoadUserComboBox()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_user, nama FROM [User]", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataRow row = dt.NewRow();
                row["id_user"] = 0;
                row["nama"] = "-- Semua User --";
                dt.Rows.InsertAt(row, 0);

                cmbUser.DataSource = dt;
                cmbUser.DisplayMember = "nama";
                cmbUser.ValueMember = "id_user";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Ambil semua data aktivitas dari view, filter berdasarkan tanggal aktivitas dan user
                string query = @"SELECT 
                    nama_user       AS [Nama User],
                    nama_olahraga   AS [Olahraga],
                    kalori_per_menit AS [Kalori/Menit],
                    durasi_menit    AS [Durasi (Menit)],
                    total_kalori    AS [Total Kalori],
                    tanggal         AS [Tanggal]
                FROM vw_RiwayatAktivitas
                WHERE tanggal >= @dari AND tanggal <= @sampai";
                
                if (cmbUser.SelectedValue != null && Convert.ToInt32(cmbUser.SelectedValue) > 0)
                {
                    query += " AND id_user = @id_user";
                }

                query += " ORDER BY tanggal DESC, nama_user";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dari", dtpDariTanggal.Value.Date);
                cmd.Parameters.AddWithValue("@sampai", dtpSampaiTanggal.Value.Date);

                if (cmbUser.SelectedValue != null && Convert.ToInt32(cmbUser.SelectedValue) > 0)
                {
                    cmd.Parameters.AddWithValue("@id_user", Convert.ToInt32(cmbUser.SelectedValue));
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Tampilkan info jumlah data & total kalori di title bar
                int totalKalori = 0;
                foreach (DataRow r in dt.Rows)
                    totalKalori += Convert.ToInt32(r["Total Kalori"]);
                
                this.Text = $"Rekap Data Aktivitas — {dt.Rows.Count} aktivitas | Total Kalori: {totalKalori:N0}";
                
                // Info filter yang digunakan
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"Tidak ada data ditemukan!\n\n" +
                        $"Filter yang digunakan:\n" +
                        $"- Dari Tanggal : {dtpDariTanggal.Value.Date:dd/MM/yyyy}\n" +
                        $"- Sampai Tanggal: {dtpSampaiTanggal.Value.Date:dd/MM/yyyy}\n\n" +
                        $"Pastikan rentang tanggal mencakup data aktivitas Anda.\n" +
                        $"Contoh: gunakan Dari = 01/04/2026 dan Sampai = 30/06/2026",
                        "Data Tidak Ditemukan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void btnCetakRekap_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Gunakan query yang sama seperti Tampilkan agar data Crystal Report konsisten
                // PENTING: Tidak boleh ada alias agar nama kolom cocok persis dengan field di file .rpt
                string query = @"SELECT 
                    nama_user,
                    nama_olahraga,
                    kalori_per_menit,
                    durasi_menit,
                    total_kalori,
                    tanggal
                FROM vw_RiwayatAktivitas
                WHERE tanggal >= @dari AND tanggal <= @sampai";
                
                if (cmbUser.SelectedValue != null && Convert.ToInt32(cmbUser.SelectedValue) > 0)
                {
                    query += " AND id_user = @id_user";
                }

                query += " ORDER BY tanggal DESC, nama_user";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dari", dtpDariTanggal.Value.Date);
                cmd.Parameters.AddWithValue("@sampai", dtpSampaiTanggal.Value.Date);

                if (cmbUser.SelectedValue != null && Convert.ToInt32(cmbUser.SelectedValue) > 0)
                {
                    cmd.Parameters.AddWithValue("@id_user", Convert.ToInt32(cmbUser.SelectedValue));
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Gunakan ReportDocument generik karena Visual Studio belum men-generate class LaporanAktivitas.cs
                CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rpt.Load(Application.StartupPath + "\\LaporanAktivitas.rpt"); 
                rpt.SetDataSource(dt);
                
                FormCetakLaporan frmCetak = new FormCetakLaporan();
                frmCetak.crystalReportViewer1.ReportSource = rpt;
                frmCetak.crystalReportViewer1.Refresh();
                frmCetak.Show();
            }
            catch (Exception ex)
            {
                string errorMsg = "Gagal mencetak: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMsg += "\n\nDetail Tambahan: " + ex.InnerException.Message;
                }
                MessageBox.Show(errorMsg, "Error Crystal Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}
