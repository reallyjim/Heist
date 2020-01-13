using System;
using System.IO;

namespace Heist
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloadFolder = GetDownloadFolder();   

            var api = new ImageApi();
            var response = api.GetTodaysImageInfo();

            var imageData = response.Images[0];

            var split = imageData.Urlbase.Split(new[] { '=' });
            var filename = $"{split[1]}.jpg";
            var targetPath = Path.Combine(downloadFolder, filename);

            api.DownloadImage(imageData.Url, targetPath);

        }

        private static string GetDownloadFolder()
        {
            var targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Heist");

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            return targetFolder;
        }
    }
}
