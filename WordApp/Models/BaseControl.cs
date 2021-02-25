using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordApp.Models
{
    public abstract class BaseControl
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Control_ID { get; set; }
        public bool Checked { get; set; }
    }
}