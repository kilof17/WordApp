using System.Collections.Generic;

namespace WordApp.Models
{
    public class WordControl : BaseControl
    {                        
        public int Type { get; set; }
        public string Tag { get; set; }
        public List<string> Dropdown { get; set; }       
        public bool Multiline { get; set; }
    }
}

    /*TYPES:
    1- text,8-checkbox, 3-combo box ,
    6-date, 4- drop-down list,7-contorls group, 
    2-picture, 9-repeating section, 0-rich-text,
    5-building block gallery,
    https://docs.microsoft.com/en-us/dotnet/api/microsoft.office.interop.word.wdcontentcontroltype?view=word-pia */
