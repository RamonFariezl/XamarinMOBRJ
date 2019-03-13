using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMOBRJ.Models.ClassesAPI
{
    public class Attachment
    {
        public string id { get; set; }
        public string url { get; set; }
        public string filename { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public Thumbnails thumbnails { get; set; }
    }
}

