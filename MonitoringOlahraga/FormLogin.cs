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
    public partial class FormLogin : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-MQ6MDQFG\\ARBYPANGESTU;Initial Catalog=DB_MonitoringOlahraga;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                conn.Open();
                MessageBox.Show("Koneksi database berhasil!", "Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void pnlLeft_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
