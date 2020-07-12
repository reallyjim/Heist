using System.Net;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Drawing;

namespace Heist
{
    internal class ImageApi
    {
        private const string _apiUrl = "https://www.bing.com";

        public ImageResponse GetTodaysImageInfo()
        {
            var url = $"{_apiUrl}/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";

            var webResponse = request.GetResponse();


            var reader = new StreamReader(webResponse.GetResponseStream());
            var content = reader.ReadToEnd();
            webResponse.Close();

            var response = JsonConvert.DeserializeObject<ImageResponse>(content);

            return response;
        }

        public void DownloadImage(string imageUrl, string targetFilePath)
        {
            var url = $"{_apiUrl}{imageUrl}";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), targetFilePath);
            }
        }

        public Image DownloadBitmap(string imageUrl)
        {
            var url = $"{_apiUrl}{imageUrl}";

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = request.GetResponse();

            using (var remoteStream = response.GetResponseStream())
            {
                var bitmap = Image.FromStream(remoteStream);
                return bitmap;
            }
        }
    }
}
