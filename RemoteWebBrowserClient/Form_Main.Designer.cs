namespace RemoteWebBrowserClient
{
    partial class Form_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.pictureBox_controller = new System.Windows.Forms.PictureBox();
            this.ToolStripMenuItem_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_close = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_connection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox_ip = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox_port = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripMenuItem_connect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_update = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_top = new System.Windows.Forms.MenuStrip();
            this.timer_check = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_controller)).BeginInit();
            this.menuStrip_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_controller
            // 
            this.pictureBox_controller.Location = new System.Drawing.Point(0, 27);
            this.pictureBox_controller.Name = "pictureBox_controller";
            this.pictureBox_controller.Size = new System.Drawing.Size(400, 400);
            this.pictureBox_controller.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_controller.TabIndex = 0;
            this.pictureBox_controller.TabStop = false;
            this.pictureBox_controller.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_controller_MouseDown);
            this.pictureBox_controller.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_controller_MouseUp);
            // 
            // ToolStripMenuItem_file
            // 
            this.ToolStripMenuItem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_close});
            this.ToolStripMenuItem_file.Name = "ToolStripMenuItem_file";
            this.ToolStripMenuItem_file.Size = new System.Drawing.Size(43, 20);
            this.ToolStripMenuItem_file.Text = "파일";
            // 
            // ToolStripMenuItem_close
            // 
            this.ToolStripMenuItem_close.Name = "ToolStripMenuItem_close";
            this.ToolStripMenuItem_close.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_close.Text = "종료";
            this.ToolStripMenuItem_close.Click += new System.EventHandler(this.ToolStripMenuItem_close_Click);
            // 
            // ToolStripMenuItem_connection
            // 
            this.ToolStripMenuItem_connection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox_ip,
            this.toolStripTextBox_port,
            this.ToolStripMenuItem_connect,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_update});
            this.ToolStripMenuItem_connection.Name = "ToolStripMenuItem_connection";
            this.ToolStripMenuItem_connection.Size = new System.Drawing.Size(43, 20);
            this.ToolStripMenuItem_connection.Text = "연결";
            // 
            // toolStripTextBox_ip
            // 
            this.toolStripTextBox_ip.Name = "toolStripTextBox_ip";
            this.toolStripTextBox_ip.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_ip.Text = "Server IP";
            // 
            // toolStripTextBox_port
            // 
            this.toolStripTextBox_port.Name = "toolStripTextBox_port";
            this.toolStripTextBox_port.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_port.Text = "Server Port";
            // 
            // ToolStripMenuItem_connect
            // 
            this.ToolStripMenuItem_connect.Name = "ToolStripMenuItem_connect";
            this.ToolStripMenuItem_connect.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_connect.Text = "Connect";
            this.ToolStripMenuItem_connect.Click += new System.EventHandler(this.ToolStripMenuItem_connect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // ToolStripMenuItem_update
            // 
            this.ToolStripMenuItem_update.Enabled = false;
            this.ToolStripMenuItem_update.Name = "ToolStripMenuItem_update";
            this.ToolStripMenuItem_update.Size = new System.Drawing.Size(160, 22);
            this.ToolStripMenuItem_update.Text = "Update";
            this.ToolStripMenuItem_update.Click += new System.EventHandler(this.ToolStripMenuItem_update_Click);
            // 
            // menuStrip_top
            // 
            this.menuStrip_top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_file,
            this.ToolStripMenuItem_connection});
            this.menuStrip_top.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_top.Name = "menuStrip_top";
            this.menuStrip_top.Size = new System.Drawing.Size(784, 24);
            this.menuStrip_top.TabIndex = 1;
            this.menuStrip_top.Text = "menuStrip1";
            // 
            // timer_check
            // 
            this.timer_check.Interval = 2000;
            this.timer_check.Tick += new System.EventHandler(this.timer_check_Tick);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pictureBox_controller);
            this.Controls.Add(this.menuStrip_top);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip_top;
            this.Name = "Form_Main";
            this.Text = "Remote Web Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Main_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_Main_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_controller)).EndInit();
            this.menuStrip_top.ResumeLayout(false);
            this.menuStrip_top.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_controller;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_file;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_close;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_connection;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_ip;
        private System.Windows.Forms.MenuStrip menuStrip_top;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_port;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_connect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_update;
        private System.Windows.Forms.Timer timer_check;
    }
}

