namespace MinioExplorer
{
    public class MinioSetting
    {
        public string Endpoint { get; set; } = "localhost:9000";
        public string AccessKey { get; set; } = "";
        public string SecretKey { get; set; } = "";
        public string Bucket { get; set; } = "";

        public bool CanDelete { get; set; } = false;
    }
}
