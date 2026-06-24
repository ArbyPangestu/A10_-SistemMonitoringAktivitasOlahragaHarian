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
    public partial class FormRegister : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString = DatabaseHelper.GetConnectionString();

        public FormRegister()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(txtNama.Text))
            {
                MessageBox.Show("Nama harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNama.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password dan Konfirmasi Password tidak sama!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

              
                SqlCommand checkCmd = new SqlCommand("sp_CheckUsername", conn);
                checkCmd.CommandType = CommandType.StoredProcedure;
                checkCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Username sudah digunakan! Silakan pilih username lain.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                
                SqlCommand insertCmd = new SqlCommand("sp_RegisterUser", conn);
                insertCmd.CommandType = CommandType.StoredProcedure;
                insertCmd.Parameters.AddWithValue("@nama", txtNama.Text);
                insertCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                insertCmd.Parameters.AddWithValue("@email", txtEmail.Text);
                insertCmd.Parameters.AddWithValue("@password", txtPassword.Text);

                int result = insertCmd.ExecuteNonQuery();

                if (result > 0)
                {
                    conn.Close();
                    MessageBox.Show("Registrasi berhasil! Silakan login dengan akun Anda.", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
