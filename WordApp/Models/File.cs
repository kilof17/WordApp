using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordApp.Models
{

    //public class Files
    //{
    //    public static List<File> FilesList { get; set; } = new List<File>();
    //}
    public class File
    {
        public string FileName { get; set; }
        public string FilePaht { get; set; }
        public string Hash { get; set; }
        public string Extension { get; set; }

    }
}