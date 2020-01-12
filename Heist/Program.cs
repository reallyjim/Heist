using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Heist
{
    class Program
    {
        static void Main(string[] args)
        {
            var finder = new FileFinder();

            var path = finder.FindFilePath();
            var fileName = finder.GetFileNameFromPath(path);

            var localPath = ConfigurationManager.AppSettings["dlPath"].ToString();
            var localFile = string.Format(localPath, fileName);

            var downloader = new FileRetriever();
            downloader.DownloadFile(path, localFile);

        }
    }
}
