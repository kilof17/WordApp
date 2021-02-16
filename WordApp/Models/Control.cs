using System.Collections.Generic;

namespace WordApp.Models
{
    public class Control 
    {
        public int ID { get; set; } 
        public string ControlId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }                
        public int Type { get; set; }
        public string Tag { get; set; }
        public List<string> Dropdown { get; set; }
        public bool Checked { get; set;} 
        public bool Multiline { get; set; }
    }
}

    /*TYPES:
    1- text,8-checkbox, 3-combo box ,
    6-date, 4- drop-down list,7-contorls group, 
    2-picture, 9-repeating section, 0-rich-text,
    5-building block gallery,
    https://docs.microsoft.com/en-us/dotnet/api/microsoft.office.interop.word.wdcontentcontroltype?view=word-pia */
