namespace RemoteWebBrowser
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
            this.tabControl_web = new System.Windows.Forms.TabControl();
            this.tabPage_newTab = new System.Windows.Forms.TabPage();
            this.contextMenuStrip_tab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_close = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_web.SuspendLayout();
            this.contextMenuStrip_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_web
            // 
            this.tabControl_web.Controls.Add(this.tabPage_newTab);
            this.tabControl_web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_web.HotTrack = true;
            this.tabControl_web.Location = new System.Drawing.Point(0, 0);
            this.tabControl_web.Multiline = true;
            this.tabControl_web.Name = "tabControl_web";
            this.tabControl_web.SelectedIndex = 0;
            this.tabControl_web.Size = new System.Drawing.Size(784, 561);
            this.tabControl_web.TabIndex = 0;
            this.tabControl_web.SelectedIndexChanged += new System.EventHandler(this.tabControl_web_SelectedIndexChanged);
            // 
            // tabPage_newTab
            // 
            this.tabPage_newTab.Location = new System.Drawing.Point(4, 22);
            this.tabPage_newTab.Name = "tabPage_newTab";
            this.tabPage_newTab.Size = new System.Drawing.Size(776, 535);
            this.tabPage_newTab.TabIndex = 0;
            this.tabPage_newTab.Tag = "+";
            this.tabPage_newTab.Text = "새 탭";
            this.tabPage_newTab.ToolTipText = "+";
            this.tabPage_newTab.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip_tab
            // 
            this.contextMenuStrip_tab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_close});
            this.contextMenuStrip_tab.Name = "contextMenuStrip_tab";
            this.contextMenuStrip_tab.Size = new System.Drawing.Size(167, 26);
            // 
            // ToolStripMenuItem_close
            // 
            this.ToolStripMenuItem_close.Name = "ToolStripMenuItem_close";
            this.ToolStripMenuItem_close.Size = new System.Drawing.Size(166, 22);
            this.ToolStripMenuItem_close.Text = "현재 페이지 닫기";
            this.ToolStripMenuItem_close.Click += new System.EventHandler(this.ToolStripMenuItem_close_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl_web);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Remote Web Browser";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.tabControl_web.ResumeLayout(false);
            this.contextMenuStrip_tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_web;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_tab;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_close;
        private System.Windows.Forms.TabPage tabPage_newTab;
    }
}

