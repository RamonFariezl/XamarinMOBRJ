using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMOBRJ.Models
{
    public class Estados
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string ImgUrl { get; set; }
        public string Capital { get; set; }
        public string Sigla { get; set; }
        public string Estado { get; set; }
    }
}
