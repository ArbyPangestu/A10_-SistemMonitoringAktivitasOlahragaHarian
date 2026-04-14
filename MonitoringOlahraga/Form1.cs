using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitoringOlahraga
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNavActivity_Click(object sender, EventArgs e)
        {
            FormAktivitasOlahraga frm = new FormAktivitasOlahraga();
            frm.Show();
        }

        private void btnNavUser_Click(object sender, EventArgs e)
        {
            FormUser frm = new FormUser();
            frm.Show();
        }

        private void btnNavSports_Click(object sender, EventArgs e)
        {
            FormJenisOlahraga frm = new FormJenisOlahraga();
            frm.Show();
        }

        private void btnNavReports_Click(object sender, EventArgs e)
        {
            FormLaporan frm = new FormLaporan();
            frm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                FormLogin frmLogin = new FormLogin();
                frmLogin.Show();
                this.Close();
            }
        }
    }
}
