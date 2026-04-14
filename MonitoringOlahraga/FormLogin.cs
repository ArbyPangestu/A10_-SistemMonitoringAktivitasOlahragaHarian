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
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = "SELECT * FROM [User] WHERE username = @username AND password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string namaUser = reader["nama"].ToString();
                    reader.Close();
                    conn.Close();

                    MessageBox.Show("Login berhasil! Selamat datang, " + namaUser + ".", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form1 dashboard = new Form1();
                    dashboard.Show();
                    this.Hide();
                    dashboard.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister frmRegister = new FormRegister();
            frmRegister.ShowDialog();
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
