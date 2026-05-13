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
    public partial class FormUser : Form
    {
        private int _selectedIdUser = 0;
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=LAPTOP-MQ6MDQFG\\ARBYPANGESTU;Initial Catalog=DB_MonitoringOlahraga;Integrated Security=True";

        public FormUser()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("User");

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
                string query = "SELECT * FROM [User]";
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;

                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;

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
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (string.IsNullOrEmpty(txtNama.Text))
                {
                    MessageBox.Show("Nama harus diisi");
                    txtNama.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbRole.Text))
                {
                    MessageBox.Show("Role harus dipilih");
                    cmbRole.Focus();
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_InsertUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data user berhasil ditambahkan");
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
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (_selectedIdUser == 0)
                {
                    MessageBox.Show("Pilih data dari tabel terlebih dahulu.");
                    return;
                }

                SqlCommand cmd = new SqlCommand("sp_UpdateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_user", _selectedIdUser);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

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
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (_selectedIdUser == 0)
                {
                    MessageBox.Show("Pilih data dari tabel terlebih dahulu.");
                    return;
                }

                DialogResult resultConfirm = MessageBox.Show(
                    "Yakin ingin menghapus data?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultConfirm == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_user", _selectedIdUser);

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

                _selectedIdUser = Convert.ToInt32(row.Cells["id_user"].Value);
                txtNama.Text = row.Cells["nama"].Value.ToString();
                txtUsername.Text = row.Cells["username"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtPassword.Text = row.Cells["password"].Value.ToString();
            }
        }

        private void ClearForm()
        {
            _selectedIdUser = 0;
            txtNama.Clear();
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtNama.Focus();
        }
    }
}
