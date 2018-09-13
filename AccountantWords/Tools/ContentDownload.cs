using System.Net;
using System.Text;

namespace AccountantWords.Tools
{
    public class ContentDownload
    {
        public static string GetXml(string url)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                return client.DownloadString(url);
            }
        }
    }
}
