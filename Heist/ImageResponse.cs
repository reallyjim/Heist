namespace Heist
{
    internal class ImageResponse
    {
        public ImageData[] Images { get; set; }
    }

    internal class ImageData
    {
        // trimming down the actual response to just what I want
        public string Url { get; set; }
        public string Urlbase { get; set; }
        public string Copyright { get; set; }
        public string Copyrightlink { get; set; }
        public string Title { get; set; }
    }

}
