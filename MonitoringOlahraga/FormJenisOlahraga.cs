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
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
          
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
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
