using System.ComponentModel.DataAnnotations;

namespace WordApp.Models
{
    [MetadataType(typeof(FormatValidation))]
    public partial class Format
    {
        public int Id { get; set; }
        public string Displayed_Name { get; set; }
        public string Format_String { get; set; }
        public int Format_Int { get; set; }
    }

    public partial class FormatValidation
    {
        public int Id { get; set; }
        [Display(Name ="Wyświetlana nazwa")]
        public string Displayed_Name { get; set; }
        [Display(Name ="Format")]
        public string Format_String { get; set; }
        [Display(Name = "Format Int")]
        public int Format_Int { get; set; }
    }

    //https://docs.microsoft.com/pl-pl/dotnet/api/microsoft.office.interop.word.wdsaveformat?view=word-pia  - all formats
}

