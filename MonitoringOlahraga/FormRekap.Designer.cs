namespace MonitoringOlahraga
{
    partial class FormRekap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnCetakRekap = new System.Windows.Forms.Button();
            this.btnTampilkan = new System.Windows.Forms.Button();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.dtpSampaiTanggal = new System.Windows.Forms.DateTimePicker();
            this.lblSampaiTanggal = new System.Windows.Forms.Label();
            this.dtpDariTanggal = new System.Windows.Forms.DateTimePicker();
            this.lblDariTanggal = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Rekap Data Aktivitas Olahraga";
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.btnCetakRekap);
            this.pnlControls.Controls.Add(this.btnTampilkan);
            this.pnlControls.Controls.Add(this.cmbUser);
            this.pnlControls.Controls.Add(this.lblUser);
            this.pnlControls.Controls.Add(this.dtpSampaiTanggal);
            this.pnlControls.Controls.Add(this.lblSampaiTanggal);
            this.pnlControls.Controls.Add(this.dtpDariTanggal);
            this.pnlControls.Controls.Add(this.lblDariTanggal);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 60);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Padding = new System.Windows.Forms.Padding(10);
            this.pnlControls.Size = new System.Drawing.Size(900, 60);
            this.pnlControls.TabIndex = 1;
            // 
            // btnCetakRekap
            // 
            this.btnCetakRekap.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnCetakRekap.FlatAppearance.BorderSize = 0;
            this.btnCetakRekap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCetakRekap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCetakRekap.ForeColor = System.Drawing.Color.White;
            this.btnCetakRekap.Location = new System.Drawing.Point(740, 15);
            this.btnCetakRekap.Name = "btnCetakRekap";
            this.btnCetakRekap.Size = new System.Drawing.Size(100, 30);
            this.btnCetakRekap.TabIndex = 7;
            this.btnCetakRekap.Text = "Cetak Rekap";
            this.btnCetakRekap.UseVisualStyleBackColor = false;
            this.btnCetakRekap.Click += new System.EventHandler(this.btnCetakRekap_Click);
            // 
            // btnTampilkan
            // 
            this.btnTampilkan.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTampilkan.FlatAppearance.BorderSize = 0;
            this.btnTampilkan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTampilkan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTampilkan.ForeColor = System.Drawing.Color.White;
            this.btnTampilkan.Location = new System.Drawing.Point(630, 15);
            this.btnTampilkan.Name = "btnTampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(100, 30);
            this.btnTampilkan.TabIndex = 6;
            this.btnTampilkan.Text = "Tampilkan";
            this.btnTampilkan.UseVisualStyleBackColor = false;
            this.btnTampilkan.Click += new System.EventHandler(this.btnTampilkan_Click);
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(460, 18);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(150, 23);
            this.cmbUser.TabIndex = 5;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(420, 21);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(36, 15);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "User :";
            // 
            // dtpSampaiTanggal
            // 
            this.dtpSampaiTanggal.CustomFormat = "dd/MM/yyyy";
            this.dtpSampaiTanggal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSampaiTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSampaiTanggal.Location = new System.Drawing.Point(300, 18);
            this.dtpSampaiTanggal.Name = "dtpSampaiTanggal";
            this.dtpSampaiTanggal.Size = new System.Drawing.Size(100, 23);
            this.dtpSampaiTanggal.TabIndex = 3;
            // 
            // lblSampaiTanggal
            // 
            this.lblSampaiTanggal.AutoSize = true;
            this.lblSampaiTanggal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampaiTanggal.Location = new System.Drawing.Point(200, 21);
            this.lblSampaiTanggal.Name = "lblSampaiTanggal";
            this.lblSampaiTanggal.Size = new System.Drawing.Size(96, 15);
            this.lblSampaiTanggal.TabIndex = 2;
            this.lblSampaiTanggal.Text = "Sampai Tanggal :";
            // 
            // dtpDariTanggal
            // 
            this.dtpDariTanggal.CustomFormat = "dd/MM/yyyy";
            this.dtpDariTanggal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDariTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDariTanggal.Location = new System.Drawing.Point(90, 18);
            this.dtpDariTanggal.Name = "dtpDariTanggal";
            this.dtpDariTanggal.Size = new System.Drawing.Size(100, 23);
            this.dtpDariTanggal.TabIndex = 1;
            // 
            // lblDariTanggal
            // 
            this.lblDariTanggal.AutoSize = true;
            this.lblDariTanggal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDariTanggal.Location = new System.Drawing.Point(10, 21);
            this.lblDariTanggal.Name = "lblDariTanggal";
            this.lblDariTanggal.Size = new System.Drawing.Size(79, 15);
            this.lblDariTanggal.TabIndex = 0;
            this.lblDariTanggal.Text = "Dari Tanggal :";
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.dataGridView1);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 120);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBody.Size = new System.Drawing.Size(900, 380);
            this.pnlBody.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(880, 360);
            this.dataGridView1.TabIndex = 0;
            // 
            // FormRekap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FormRekap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rekap Data Aktivitas Olahraga";
            this.Load += new System.EventHandler(this.FormRekap_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Label lblDariTanggal;
        private System.Windows.Forms.DateTimePicker dtpDariTanggal;
        private System.Windows.Forms.Label lblSampaiTanggal;
        private System.Windows.Forms.DateTimePicker dtpSampaiTanggal;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Button btnTampilkan;
        private System.Windows.Forms.Button btnCetakRekap;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
