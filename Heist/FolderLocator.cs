using System;
using System.Configuration;
using System.IO;

namespace Heist
{
    public class FolderLocator
    {
        public string GetDownloadFolder()
        {
            var dlConfig = ConfigurationManager.AppSettings["dlPath"];

            if (string.IsNullOrWhiteSpace(dlConfig)) return GetDefaultFolder();
            
            // make sure this is a real folder
            return Directory.Exists(dlConfig) ? dlConfig : GetDefaultFolder();
        }
        
        private static string GetDefaultFolder()
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