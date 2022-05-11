using Minio;
using Minio.DataModel.Tracing;
using System.Net;
using System.Text.RegularExpressions;

namespace MinioExplorer
{
    public class MinioRequestLogger : IRequestLogger
    { 
        public Action<int> PartLogAction { get; set; }

        public void LogRequest(RequestToLog requestToLog, ResponseToLog responseToLog, double durationMs)
        {
            if (responseToLog.statusCode == HttpStatusCode.OK)
            {
                var match = Regex.Match(requestToLog.uri.Query, @"partNumber=(?<partNumber>\d+)");
                if (match.Success)
                {
                    int partNumber = Convert.ToInt32(match.Groups["partNumber"].Value);
                    PartLogAction.Invoke(partNumber);
                }
            }
        }
    }
}
