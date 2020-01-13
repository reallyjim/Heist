using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Heist
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloadFolder = DownloadFolder();   
            GetFileTheNewWay(downloadFolder);
        }

        private static void GetFileTheNewWay(string folderName)
        {
            var api = new ImageApi();
            var response = api.GetTodaysImageInfo();

            var imageData = response.Images[0];

            var split = imageData.Urlbase.Split(new[] { '=' }); //, '_' });
            var filename = $"{split[1]}.jpg";
            var targetPath = Path.Combine(folderName, filename);

            api.DownloadImage(imageData.Url, targetPath);

            
            // var image = api.DownloadBitmap(imageData.Url);
            //SetExtendedProperties(image, imageData, targetPath);
        }

        private static void SetExtendedProperties(Image image, ImageData imageData, string targetPath)
        {
            image.AddMetaProperty(MetaProperty.Title, imageData.Title);
            image.AddMetaProperty(MetaProperty.Copyright, imageData.Copyright);
            image.Save(targetPath);
        }



        private static void GetFileTheOldWay()
        {
            var finder = new FileFinder();

            var path = finder.FindFilePath();
            var fileName = finder.GetFileNameFromPath(path);

            var localPath = ConfigurationManager.AppSettings["dlPath"].ToString();
            var localFile = string.Format(localPath, fileName);

            var downloader = new FileRetriever();
            downloader.DownloadFile(path, localFile);
        }

        private static string DownloadFolder()
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
