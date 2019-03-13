using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMOBRJ.Models.ClassesAPI
{
    public class Fields
    {
        public string Sigla { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string Estado { get; set; }
        public string Capital { get; set; }
        public string Regiao { get; set; }


        public string ImgUrl
        {
            get
            {
                return Attachments[0].thumbnails.small.url;
            }
            set {; }
        }
        //public Fields()
        //{
        //    Attachments = new List<Attachment>();
        //}
    }
}
