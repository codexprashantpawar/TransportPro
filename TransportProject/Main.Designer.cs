namespace TransportProject
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.transportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routeMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.busStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignTransportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAdmissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelAdmissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.LogOut = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transportToolStripMenuItem,
            this.masterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1404, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // transportToolStripMenuItem
            // 
            this.transportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.routeMasterToolStripMenuItem,
            this.stopMasterToolStripMenuItem,
            this.busStopToolStripMenuItem,
            this.assignTransportToolStripMenuItem});
            this.transportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transportToolStripMenuItem.Name = "transportToolStripMenuItem";
            this.transportToolStripMenuItem.Size = new System.Drawing.Size(126, 34);
            this.transportToolStripMenuItem.Text = "Transport";
            // 
            // routeMasterToolStripMenuItem
            // 
            this.routeMasterToolStripMenuItem.Name = "routeMasterToolStripMenuItem";
            this.routeMasterToolStripMenuItem.Size = new System.Drawing.Size(261, 34);
            this.routeMasterToolStripMenuItem.Text = "Route Master";
            this.routeMasterToolStripMenuItem.Click += new System.EventHandler(this.routeMasterToolStripMenuItem_Click);
            // 
            // stopMasterToolStripMenuItem
            // 
            this.stopMasterToolStripMenuItem.Name = "stopMasterToolStripMenuItem";
            this.stopMasterToolStripMenuItem.Size = new System.Drawing.Size(261, 34);
            this.stopMasterToolStripMenuItem.Text = "Stop Master";
            this.stopMasterToolStripMenuItem.Click += new System.EventHandler(this.stopMasterToolStripMenuItem_Click);
            // 
            // busStopToolStripMenuItem
            // 
            this.busStopToolStripMenuItem.Name = "busStopToolStripMenuItem";
            this.busStopToolStripMenuItem.Size = new System.Drawing.Size(261, 34);
            this.busStopToolStripMenuItem.Text = "Bus Stop";
            this.busStopToolStripMenuItem.Click += new System.EventHandler(this.busStopToolStripMenuItem_Click);
            // 
            // assignTransportToolStripMenuItem
            // 
            this.assignTransportToolStripMenuItem.Name = "assignTransportToolStripMenuItem";
            this.assignTransportToolStripMenuItem.Size = new System.Drawing.Size(261, 34);
            this.assignTransportToolStripMenuItem.Text = "Assign Transport";
            this.assignTransportToolStripMenuItem.Click += new System.EventHandler(this.assignTransportToolStripMenuItem_Click);
            // 
            // masterToolStripMenuItem
            // 
            this.masterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAdmissionToolStripMenuItem,
            this.cancelAdmissionToolStripMenuItem});
            this.masterToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            this.masterToolStripMenuItem.Size = new System.Drawing.Size(98, 34);
            this.masterToolStripMenuItem.Text = "Master";
            // 
            // newAdmissionToolStripMenuItem
            // 
            this.newAdmissionToolStripMenuItem.Name = "newAdmissionToolStripMenuItem";
            this.newAdmissionToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
            this.newAdmissionToolStripMenuItem.Text = "New Admission";
            this.newAdmissionToolStripMenuItem.Click += new System.EventHandler(this.newAdmissionToolStripMenuItem_Click);
            // 
            // cancelAdmissionToolStripMenuItem
            // 
            this.cancelAdmissionToolStripMenuItem.Name = "cancelAdmissionToolStripMenuItem";
            this.cancelAdmissionToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
            this.cancelAdmissionToolStripMenuItem.Text = "Cancel Admission";
            this.cancelAdmissionToolStripMenuItem.Click += new System.EventHandler(this.cancelAdmissionToolStripMenuItem_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1404, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // LogOut
            // 
            this.LogOut.BackColor = System.Drawing.Color.IndianRed;
            this.LogOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogOut.Location = new System.Drawing.Point(1153, 0);
            this.LogOut.Name = "LogOut";
            this.LogOut.Size = new System.Drawing.Size(132, 50);
            this.LogOut.TabIndex = 2;
            this.LogOut.Text = "LogOut";
            this.LogOut.UseVisualStyleBackColor = false;
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TransportProject.Properties.Resources.bus;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1404, 779);
            this.Controls.Add(this.LogOut);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem transportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routeMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem busStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignTransportToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem masterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAdmissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelAdmissionToolStripMenuItem;
        private System.Windows.Forms.Button LogOut;

    }
}