using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MonitoringOlahraga
{
    public partial class FormLaporan : Form
    {
        private readonly string connectionString = "Data Source=LAPTOP-MQ6MDQFG\\ARBYPANGESTU;Initial Catalog=DB_MonitoringOlahraga;Integrated Security=True";

        public FormLaporan()
        {
            InitializeComponent();
        }

        private void FormLaporan_Load(object sender, EventArgs e)
        {
            // Set agar Grid hanya bisa dilihat (Validasi Revisi)
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Query untuk mengambil data laporan dari View
                string query = "SELECT id_laporan, nama_user, periode_awal, periode_akhir, total_keseluruhan_kalori, tanggal_dibuat FROM vw_DataLaporan ORDER BY tanggal_dibuat DESC";

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                // Pengaturan DataGridView & Binding
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridView2.DataSource = bs;
                bindingNavigator1.BindingSource = bs;

                // Styling Premium
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
                dataGridView2.DefaultCellStyle.SelectionForeColor = Color.White;
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.ColumnHeadersVisible = true;

                // Sembunyikan kolom ID
                if (dataGridView2.Columns["id_laporan"] != null) dataGridView2.Columns["id_laporan"].Visible = false;
                if (dataGridView2.Columns["id_user"] != null) dataGridView2.Columns["id_user"].Visible = false;

                // Merapikan nama header dan format data
                if (dataGridView2.Columns["periode_awal"] != null)
                {
                    dataGridView2.Columns["periode_awal"].HeaderText = "Periode Awal";
                    dataGridView2.Columns["periode_awal"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dataGridView2.Columns["periode_akhir"] != null)
                {
                    dataGridView2.Columns["periode_akhir"].HeaderText = "Periode Akhir";
                    dataGridView2.Columns["periode_akhir"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dataGridView2.Columns["total_keseluruhan_kalori"] != null)
                {
                    dataGridView2.Columns["total_keseluruhan_kalori"].HeaderText = "Total Kalori";
                    dataGridView2.Columns["total_keseluruhan_kalori"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dataGridView2.Columns["tanggal_dibuat"] != null)
                {
                    dataGridView2.Columns["tanggal_dibuat"].HeaderText = "Tanggal Dibuat";
                    dataGridView2.Columns["tanggal_dibuat"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

                dataGridView2.Refresh();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data laporan yang tersedia.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
