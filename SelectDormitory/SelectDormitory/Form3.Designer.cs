namespace SelectDormitory
{
    partial class Form3
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
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.填写作息习惯ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择宿舍ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看自己的宿舍ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.修改密码ToolStripMenuItem,
            this.填写作息习惯ToolStripMenuItem,
            this.选择宿舍ToolStripMenuItem,
            this.查看自己的宿舍ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(873, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(146, 34);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 填写作息习惯ToolStripMenuItem
            // 
            this.填写作息习惯ToolStripMenuItem.Name = "填写作息习惯ToolStripMenuItem";
            this.填写作息习惯ToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.填写作息习惯ToolStripMenuItem.Text = "填写作息习惯";
            this.填写作息习惯ToolStripMenuItem.Click += new System.EventHandler(this.填写作息习惯ToolStripMenuItem_Click);
            // 
            // 选择宿舍ToolStripMenuItem
            // 
            this.选择宿舍ToolStripMenuItem.Name = "选择宿舍ToolStripMenuItem";
            this.选择宿舍ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.选择宿舍ToolStripMenuItem.Text = "选择宿舍";
            this.选择宿舍ToolStripMenuItem.Click += new System.EventHandler(this.选择宿舍ToolStripMenuItem_Click);
            // 
            // 查看自己的宿舍ToolStripMenuItem
            // 
            this.查看自己的宿舍ToolStripMenuItem.Name = "查看自己的宿舍ToolStripMenuItem";
            this.查看自己的宿舍ToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.查看自己的宿舍ToolStripMenuItem.Text = "查看自己的宿舍";
            this.查看自己的宿舍ToolStripMenuItem.Click += new System.EventHandler(this.查看自己的宿舍ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 52);
            this.label1.TabIndex = 1;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 673);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form3";
            this.Text = "学生信息中心";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 填写作息习惯ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择宿舍ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看自己的宿舍ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}