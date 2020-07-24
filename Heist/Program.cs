using System;
using System.IO;

namespace Heist
{
    static class Program
    {
        static void Main(string[] args)
        {
            var folderFinder = new FolderLocator();
            var downloadFolder = folderFinder.GetDownloadFolder();   

            var api = new ImageApi();
            var response = api.GetTodaysImageInfo();

            if (response.HasImage)
            {
                var imageData = response.Images[0];

                var split = imageData.Urlbase.Split(new[] { '=' });
                var filename = $"{split[1]}.jpg";
                var targetPath = Path.Combine(downloadFolder, filename);

                api.DownloadImage(imageData.Url, targetPath);    
            }
        }
    }
}
