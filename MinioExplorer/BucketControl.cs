using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using System.Diagnostics;
using System.Reactive.Linq;

namespace MinioExplorer
{
    public partial class BucketControl : UserControl
    {
        private readonly MinioSetting _minioSetting;
        private readonly MinioClient _minio;
        private readonly Stack<string> _dirStack = new Stack<string>();
        private readonly string DownloadDir = "Download";

        public BucketControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            tsp_progress.Style = ProgressBarStyle.Marquee;
            tsp_progress.Visible = false;
            tss_info.Available = false;
        }

        public BucketControl(MinioSetting minioSetting) : this()
        {
            _minioSetting = minioSetting;

            _minio = new MinioClient()
               .WithEndpoint(minioSetting.Endpoint)
               .WithCredentials(minioSetting.AccessKey, minioSetting.SecretKey)
               .Build();
        }

        private void BucketControl_Load(object sender, EventArgs e)
        {
            ChangeTssVisible();
            LoadFiles();
        }

        /// <summary>
        /// 进入指定Minio目录
        /// </summary>
        /// <param name="dir"></param>
        private void EnterDir(string dir)
        {
            _dirStack.Push(tss_currentDir.Text);
            ChangeTssVisible();
            LoadFiles(dir);
        }

        /// <summary>
        /// 价格指定目录文件列表
        /// </summary>
        /// <param name="dir"></param>
        private void LoadFiles(string dir = "")
        {
            UpdateCurrentDir(dir);

            try
            {
                Task.Run(() => {
                    var args = new ListObjectsArgs().WithBucket(_minioSetting.Bucket).WithPrefix(dir);
                    var items = _minio.ListObjectsAsync(args).ToList().Wait();
                    RefreshData(items);
                });
            }
            catch (MinioException e)
            {
            }
            
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="paths"></param>
        private void UploadFile(string[] paths)
        {
            if (paths.Length <= 0)
            {
                return;
            }
            Task.Run(() =>
            {
                try
                {
                    var currentDir = "";
                    if (_dirStack.Any())
                    {
                        currentDir = _dirStack.Peek();
                    }

                    var allFiles = new List<string>();
                    foreach (var path in paths)
                    {
                        var files = GetAllFiles(path);
                        allFiles.AddRange(files);
                    }

                    var dir = Path.GetDirectoryName(paths[0]);

                    int index = 0;
                    int maxIndex = allFiles.Count;
                    foreach (var file in allFiles)
                    {
                        ++index;
                        var objectName = currentDir + file.Substring(dir.Length + 1).Replace('\\', '/');
                        var message = $"正在上传({index}/{maxIndex}):{objectName}";

                        var putObjectArgs = new PutObjectArgs().WithBucket(_minioSetting.Bucket).WithObject(objectName).WithFileName(file);

                        if (allFiles.Count == 1)
                        {
                            var fileLength = new FileInfo(file).Length;
                            maxIndex = (int)Math.Ceiling(fileLength / (1024.0 * 1024 * 10));
                            UpdateProgress(0, maxIndex, message);

                            _minio.SetTraceOn(new MinioRequestLogger()
                            {
                                PartLogAction = part =>
                                {
                                    UpdateProgress(part, maxIndex, message);
                                }
                            });
                        }
                        else
                        {
                            UpdateProgress(index, maxIndex, message);
                        }
                        
                        _minio.PutObjectAsync(putObjectArgs).Wait();
                    }

                    LoadFiles(tss_currentDir.Text);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    Task.Delay(100).Wait();
                    HideProgress();
                }
            });
        }

        /// <summary>
        /// 获取指定minio路径下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<string> GetAllFiles(string path)
        {
            var result = new List<string>();

            if (File.Exists(path))
            {
                result.Add(path);
                return result;
            }


            var files = Directory.GetFiles(path);
            result.AddRange(files);

            foreach (var item in Directory.GetDirectories(path))
            {
                result.AddRange(GetAllFiles(item));
            }

            return result;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="item"></param>
        private async void DownloadFile(Item item)
        {
            if (item.IsDir)
            {
                return;
            }
            try
            {
                var fileName = item.Key.Replace('/', '\\');
                var filePath = Path.Combine(DownloadDir, _minioSetting.Bucket, fileName);
                if (File.Exists(filePath) && new FileInfo(filePath).CreationTime > item.LastModifiedDateTime)
                {
                    OpenDir();
                    return;
                }

                var dir = Path.GetDirectoryName(filePath);
                Directory.CreateDirectory(dir);

                var message = $"正在下载:{item.Key}";
                ShowProgress(message);

                GetObjectArgs args = new GetObjectArgs()
                                                    .WithBucket(_minioSetting.Bucket)
                                                    .WithObject(item.Key)
                                                    .WithFile(filePath);
                await _minio.GetObjectAsync(args);
                OpenDir();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                Task.Delay(100).Wait();
                HideProgress();
            }
            
        }

        /// <summary>
        /// 下载指定目录下的所有文件
        /// </summary>
        /// <param name="item"></param>
        private async void DownloadDirFiles(Item item)
        {
            try
            {
                var listObjectsArgs = new ListObjectsArgs().WithBucket(_minioSetting.Bucket).WithPrefix(item.Key).WithRecursive(true);
                var items = _minio.ListObjectsAsync(listObjectsArgs).ToList().Wait();
                int index = 0;
                int maxIndex = items.Count;
                foreach (var itemObj in items)
                {
                    ++index;
                    var fileName = itemObj.Key.Replace('/', '\\');
                    var filePath = Path.Combine(DownloadDir, _minioSetting.Bucket, fileName);
                    if (File.Exists(filePath) && new FileInfo(filePath).CreationTime > itemObj.LastModifiedDateTime)
                    {
                        // 已经存在新的了
                        continue;
                    }

                    var dir = Path.GetDirectoryName(filePath);
                    Directory.CreateDirectory(dir);

                    var message = $"正在下载({index}/{maxIndex}):{itemObj.Key}";
                    UpdateProgress(index, maxIndex, message);
                    GetObjectArgs args = new GetObjectArgs()
                                                        .WithBucket(_minioSetting.Bucket)
                                                        .WithObject(itemObj.Key)
                                                        .WithFile(filePath);
                    await _minio.GetObjectAsync(args);
                }
                
                OpenDir(item.Key);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                Task.Delay(100).Wait();
                HideProgress();
            }

        }

        /// <summary>
        /// 修改当前目录显示状态
        /// </summary>
        private void ChangeTssVisible()
        {
            tss_currentDir.Available = _dirStack.Count > 0;
        }

        /// <summary>
        /// 打开本地目录
        /// </summary>
        /// <param name="dir"></param>
        private void OpenDir(string? dir = null)
        {
            if (string.IsNullOrEmpty(dir))
            {
                dir = tss_currentDir.Text;
            }
            var fileName = dir.Replace('/', '\\');
            var filePath = Path.Combine(DownloadDir, _minioSetting.Bucket, fileName);
            Process.Start("explorer.exe", filePath);
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="message"></param>
        private void UpdateProgress(int value, int max, string message = "")
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => UpdateProgress(value, max, message)));
                return;
            }
            if (value > max)
            {
                max = value;
            }

            tss_info.Text = message;
            tss_info.Available = true;
            tss_info.Visible = true;

            tsp_progress.Style = ProgressBarStyle.Blocks;
            tsp_progress.Maximum = max;
            tsp_progress.Value = value;
            tsp_progress.Available = true;
            tsp_progress.Visible = true;
        }

        /// <summary>
        /// 隐藏进度
        /// </summary>
        private void HideProgress()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => HideProgress()));
                return;
            }
            tsp_progress.Available = false;
            tsp_progress.Visible = false;
            tss_info.Available = false;
        }

        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="message"></param>
        private void ShowProgress(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => ShowProgress(message)));
                return;
            }
            tsp_progress.Style = ProgressBarStyle.Marquee;
            tsp_progress.Available = true;
            tsp_progress.Visible = true;
            tss_info.Available = true;
            tss_info.Text = message;
        }

        /// <summary>
        /// 更新当前目录显示
        /// </summary>
        /// <param name="currentDir"></param>
        private void UpdateCurrentDir(string currentDir)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => UpdateCurrentDir(currentDir)));
                return;
            }
            tss_currentDir.Text = currentDir;
        }

        /// <summary>
        /// 刷新数据显示
        /// </summary>
        /// <param name="items"></param>
        private void RefreshData(IList<Item> items)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => RefreshData(items)));
                return;
            }

            dgv_main.Rows.Clear();
            tss_fileCount.Text = "文件:" + items.Where(t => !t.IsDir).Count();
            tss_dirCount.Text = "目录:" + items.Where(t => t.IsDir).Count();
            foreach (var item in items)
            {
                var rowIndex = dgv_main.Rows.Add(item.IsDir ? "目录" : "文件", item.Key, item.LastModifiedDateTime, item.IsDir ? null : Math.Ceiling(item.Size / 1024.0) / 1024.0, item.IsDir ? "进入" : "下载");
                var row = dgv_main.Rows[rowIndex];
                if (item.IsDir)
                {
                    row.DefaultCellStyle.ForeColor = Color.DarkGray;
                }
                row.Tag = item;
            }
        }

        private void tss_dir_Click(object sender, EventArgs e)
        {
            var dir = _dirStack.Pop();
            ChangeTssVisible();
            LoadFiles(dir);
        }

        private async void dgv_main_DragDrop(object sender, DragEventArgs e)
        {
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            UploadFile(paths);
        }

        private void dgv_main_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void dgv_main_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var row = dgv_main.Rows[e.RowIndex];
            if (row.Tag is Item item)
            {
                if (item.IsDir)
                {
                    EnterDir(item.Key);
                }
                else
                {
                    DownloadFile(item);
                }
            }
        }

        private void dgv_main_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (DataGridViewRow selectedRow in dgv_main.SelectedRows)
                {
                    selectedRow.Selected = false;
                }
                var row = dgv_main.Rows[e.RowIndex];
                row.Selected = true;
                var menu = new ContextMenuStrip();

                var btnDownload = menu.Items.Add("下载");
                var item = row.Tag as Item;
                btnDownload.Click += (s, e) => {
                    if (item.IsDir)
                    {
                        DownloadDirFiles(item);
                    }
                    else
                    {
                        DownloadFile(item);
                    }
                };
                menu.Items.Add(btnDownload);
                dgv_main.ContextMenuStrip = menu;
            }
        }
    }
}
