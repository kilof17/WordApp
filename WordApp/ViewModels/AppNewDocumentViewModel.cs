using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordApp.Models;

namespace WordApp.ViewModels
{
    public class AppNewDocumentViewModel
    {
        public List<WordControl> AllControls { get; set; }
        public List<Format> AllFormats { get; set; }
    }
}