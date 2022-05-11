namespace MinioExplorer
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tab_main = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tab_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_main
            // 
            this.tab_main.Controls.Add(this.tabPage1);
            this.tab_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_main.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tab_main.HotTrack = true;
            this.tab_main.ItemSize = new System.Drawing.Size(61, 32);
            this.tab_main.Location = new System.Drawing.Point(0, 0);
            this.tab_main.Name = "tab_main";
            this.tab_main.Padding = new System.Drawing.Point(12, 3);
            this.tab_main.SelectedIndex = 0;
            this.tab_main.Size = new System.Drawing.Size(1047, 596);
            this.tab_main.TabIndex = 0;
            this.tab_main.SelectedIndexChanged += new System.EventHandler(this.tab_main_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1039, 556);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bucket";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 596);
            this.Controls.Add(this.tab_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Minio资源管理器";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tab_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tab_main;
        private TabPage tabPage1;
    }
}