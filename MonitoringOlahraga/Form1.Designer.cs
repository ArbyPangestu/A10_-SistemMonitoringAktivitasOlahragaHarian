namespace MonitoringOlahraga
{
    partial class Form1
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNavActivity = new System.Windows.Forms.Button();
            this.btnNavUser = new System.Windows.Forms.Button();
            this.btnNavSports = new System.Windows.Forms.Button();
            this.btnNavReports = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnNavReports);
            this.pnlSidebar.Controls.Add(this.btnNavSports);
            this.pnlSidebar.Controls.Add(this.btnNavUser);
            this.pnlSidebar.Controls.Add(this.btnNavActivity);
            this.pnlSidebar.Controls.Add(this.lblTitle);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(200, 450);
            this.pnlSidebar.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(176, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Monitoring\r\nOlahraga";
            // 
            // btnNavActivity
            // 
            this.btnNavActivity.FlatAppearance.BorderSize = 0;
            this.btnNavActivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavActivity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNavActivity.ForeColor = System.Drawing.Color.LightGray;
            this.btnNavActivity.Location = new System.Drawing.Point(0, 100);
            this.btnNavActivity.Name = "btnNavActivity";
            this.btnNavActivity.Size = new System.Drawing.Size(200, 40);
            this.btnNavActivity.TabIndex = 1;
            this.btnNavActivity.Text = "Aktivitas Olahraga";
            this.btnNavActivity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavActivity.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavActivity.UseVisualStyleBackColor = true;
            this.btnNavActivity.Click += new System.EventHandler(this.btnNavActivity_Click);
            // 
            // btnNavUser
            // 
            this.btnNavUser.FlatAppearance.BorderSize = 0;
            this.btnNavUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNavUser.ForeColor = System.Drawing.Color.LightGray;
            this.btnNavUser.Location = new System.Drawing.Point(0, 140);
            this.btnNavUser.Name = "btnNavUser";
            this.btnNavUser.Size = new System.Drawing.Size(200, 40);
            this.btnNavUser.TabIndex = 2;
            this.btnNavUser.Text = "Manajemen User";
            this.btnNavUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavUser.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavUser.UseVisualStyleBackColor = true;
            this.btnNavUser.Click += new System.EventHandler(this.btnNavUser_Click);
            // 
            // btnNavSports
            // 
            this.btnNavSports.FlatAppearance.BorderSize = 0;
            this.btnNavSports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavSports.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNavSports.ForeColor = System.Drawing.Color.LightGray;
            this.btnNavSports.Location = new System.Drawing.Point(0, 180);
            this.btnNavSports.Name = "btnNavSports";
            this.btnNavSports.Size = new System.Drawing.Size(200, 40);
            this.btnNavSports.TabIndex = 3;
            this.btnNavSports.Text = "Jenis Olahraga";
            this.btnNavSports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavSports.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavSports.UseVisualStyleBackColor = true;
            this.btnNavSports.Click += new System.EventHandler(this.btnNavSports_Click);
            // 
            // btnNavReports
            // 
            this.btnNavReports.FlatAppearance.BorderSize = 0;
            this.btnNavReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavReports.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNavReports.ForeColor = System.Drawing.Color.LightGray;
            this.btnNavReports.Location = new System.Drawing.Point(0, 220);
            this.btnNavReports.Name = "btnNavReports";
            this.btnNavReports.Size = new System.Drawing.Size(200, 40);
            this.btnNavReports.TabIndex = 4;
            this.btnNavReports.Text = "Laporan Aktivitas";
            this.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavReports.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNavReports.UseVisualStyleBackColor = true;
            this.btnNavReports.Click += new System.EventHandler(this.btnNavReports_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(255, 100, 100);
            this.btnLogout.Location = new System.Drawing.Point(0, 390);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 40);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.lblWelcome);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(200, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(600, 450);
            this.pnlMain.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Light", 24F);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblWelcome.Location = new System.Drawing.Point(40, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(236, 45);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Selamat Datang!";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(44, 100);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(350, 19);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Silakan pilih menu di samping untuk mengelola data.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "Form1";
            this.MinimumSize = new System.Drawing.Size(820, 490);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitoring Olahraga - Dashboard";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnNavActivity;
        private System.Windows.Forms.Button btnNavUser;
        private System.Windows.Forms.Button btnNavSports;
        private System.Windows.Forms.Button btnNavReports;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblStatus;
    }
}
