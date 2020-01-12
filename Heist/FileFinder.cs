using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Heist
{
    public class FileFinder
    {
        public string FindFilePath()
        {
            string root = "http://www.bing.com";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(root);
            var response = request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            string content = reader.ReadToEnd();
            response.Close();

            var marker = "{url:'";
            var urlLoc = content.IndexOf(marker) + marker.Length;
            var stripped = content.Substring(urlLoc, content.Length - urlLoc);

            urlLoc = stripped.IndexOf("'");

            var path = stripped.Substring(0, urlLoc);
            path = root + path;
            return path;

        }

        public string GetFileNameFromPath(string path)
        {
            var marker = "?p=rb%2f";
            var urlLoc = path.IndexOf(marker) + marker.Length;
            var stripped = path.Substring(urlLoc, path.Length - urlLoc);

            return stripped;
        }

    }
}
