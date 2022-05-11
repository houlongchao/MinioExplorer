using Newtonsoft.Json;

namespace MinioExplorer
{
    public partial class FormMain : Form
    {
        private string _orgTitle = string.Empty;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _orgTitle = this.Text;
            tab_main.TabPages.Clear();

            if (!Directory.Exists("Config"))
            {
                Directory.CreateDirectory("Config");
            }
            var files = Directory.GetFiles("Config");
            if (files.Length <= 0)
            {
                File.WriteAllText(Path.Combine("Config","demo.json"), JsonConvert.SerializeObject(new MinioSetting() { Bucket = "demo"}, Formatting.Indented));
            }

            foreach (var file in files)
            {
                try
                {
                    var configFile = File.ReadAllText(file);
                    var config = JsonConvert.DeserializeObject<MinioSetting>(configFile);

                    var name = Path.GetFileNameWithoutExtension(file);
                    AddFileTab(name, config);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            UpdateTitle();
        }

        private void AddFileTab(string name, MinioSetting minioSetting)
        {

            tab_main.TabPages.Add(name, name);
            var bucketControl = new BucketControl(minioSetting);
            tab_main.TabPages[name].Controls.Add(bucketControl);
        }

        private void tab_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            if (tab_main.SelectedTab == null)
            {
                this.Text = _orgTitle;
                return;
            }
            this.Text = $"{_orgTitle}£¨{tab_main.SelectedTab.Text}£©";
        }
    }
}