namespace MinioExplorer
{
    partial class BucketControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_main = new System.Windows.Forms.DataGridView();
            this.txt_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_lastModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_fileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tss_currentDir = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_dirCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_fileCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsp_progress = new System.Windows.Forms.ToolStripProgressBar();
            this.tss_info = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_main
            // 
            this.dgv_main.AllowDrop = true;
            this.dgv_main.AllowUserToAddRows = false;
            this.dgv_main.AllowUserToDeleteRows = false;
            this.dgv_main.AllowUserToResizeRows = false;
            this.dgv_main.BackgroundColor = System.Drawing.Color.White;
            this.dgv_main.ColumnHeadersHeight = 29;
            this.dgv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txt_type,
            this.txt_name,
            this.txt_lastModified,
            this.txt_fileSize});
            this.dgv_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_main.Location = new System.Drawing.Point(0, 0);
            this.dgv_main.Name = "dgv_main";
            this.dgv_main.ReadOnly = true;
            this.dgv_main.RowHeadersVisible = false;
            this.dgv_main.RowHeadersWidth = 51;
            this.dgv_main.RowTemplate.Height = 29;
            this.dgv_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_main.Size = new System.Drawing.Size(611, 427);
            this.dgv_main.TabIndex = 1;
            this.dgv_main.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_main_CellDoubleClick);
            this.dgv_main.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_main_CellMouseDown);
            this.dgv_main.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_main_DragDrop);
            this.dgv_main.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_main_DragEnter);
            // 
            // txt_type
            // 
            this.txt_type.HeaderText = "类型";
            this.txt_type.MinimumWidth = 6;
            this.txt_type.Name = "txt_type";
            this.txt_type.ReadOnly = true;
            this.txt_type.Width = 60;
            // 
            // txt_name
            // 
            this.txt_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txt_name.HeaderText = "名称";
            this.txt_name.MinimumWidth = 6;
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            // 
            // txt_lastModified
            // 
            this.txt_lastModified.HeaderText = "修改时间";
            this.txt_lastModified.MinimumWidth = 6;
            this.txt_lastModified.Name = "txt_lastModified";
            this.txt_lastModified.ReadOnly = true;
            this.txt_lastModified.Width = 160;
            // 
            // txt_fileSize
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "0.000 MB";
            dataGridViewCellStyle1.NullValue = null;
            this.txt_fileSize.DefaultCellStyle = dataGridViewCellStyle1;
            this.txt_fileSize.HeaderText = "大小";
            this.txt_fileSize.MinimumWidth = 6;
            this.txt_fileSize.Name = "txt_fileSize";
            this.txt_fileSize.ReadOnly = true;
            this.txt_fileSize.Width = 130;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tss_currentDir,
            this.tss_dirCount,
            this.tss_fileCount,
            this.tsp_progress,
            this.tss_info});
            this.statusStrip.Location = new System.Drawing.Point(0, 427);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(611, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tss_currentDir
            // 
            this.tss_currentDir.BackColor = System.Drawing.Color.Green;
            this.tss_currentDir.ForeColor = System.Drawing.Color.White;
            this.tss_currentDir.Name = "tss_currentDir";
            this.tss_currentDir.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tss_currentDir.Size = new System.Drawing.Size(74, 20);
            this.tss_currentDir.Text = "当前目录";
            this.tss_currentDir.Click += new System.EventHandler(this.tss_dir_Click);
            // 
            // tss_dirCount
            // 
            this.tss_dirCount.ForeColor = System.Drawing.Color.DarkGray;
            this.tss_dirCount.Name = "tss_dirCount";
            this.tss_dirCount.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tss_dirCount.Size = new System.Drawing.Size(57, 20);
            this.tss_dirCount.Text = "目录:0";
            // 
            // tss_fileCount
            // 
            this.tss_fileCount.Name = "tss_fileCount";
            this.tss_fileCount.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tss_fileCount.Size = new System.Drawing.Size(57, 20);
            this.tss_fileCount.Text = "文件:0";
            // 
            // tsp_progress
            // 
            this.tsp_progress.Name = "tsp_progress";
            this.tsp_progress.Size = new System.Drawing.Size(100, 18);
            // 
            // tss_info
            // 
            this.tss_info.ForeColor = System.Drawing.Color.DarkOrchid;
            this.tss_info.Name = "tss_info";
            this.tss_info.Size = new System.Drawing.Size(41, 20);
            this.tss_info.Text = "a.txt";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_main);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 427);
            this.panel1.TabIndex = 3;
            // 
            // BucketControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Name = "BucketControl";
            this.Size = new System.Drawing.Size(611, 453);
            this.Load += new System.EventHandler(this.BucketControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dgv_main;
        private StatusStrip statusStrip;
        private Panel panel1;
        private ToolStripProgressBar tsp_progress;
        private ToolStripStatusLabel tss_currentDir;
        private ToolStripStatusLabel tss_fileCount;
        private ToolStripStatusLabel tss_dirCount;
        private ToolStripStatusLabel tss_info;
        private DataGridViewTextBoxColumn txt_type;
        private DataGridViewTextBoxColumn txt_name;
        private DataGridViewTextBoxColumn txt_lastModified;
        private DataGridViewTextBoxColumn txt_fileSize;
    }
}
