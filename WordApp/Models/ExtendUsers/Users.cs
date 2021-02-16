using System.ComponentModel.DataAnnotations;


namespace WordApp.Models
{   
    [MetadataType(typeof(UsersValidation))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    } 

    public class UsersValidation
    {
        [Display(Name ="Login:")] 
        [Required(AllowEmptyStrings =false,ErrorMessage ="Wpisz login")]
        [RegularExpression(@"^[a-z0-9A-Z]+$", ErrorMessage = "Pole zawiera niedozwolone znaki.")]
        [MinLength(5, ErrorMessage = "Nazwa musi zawierać minimum 5 znaków")]
        [MaxLength(20, ErrorMessage = "Nazwa może zawierać maksymalnie 20 znaków")]
        public string Nickname { get; set; }

        [Display(Name = "Imię:")]
        public string Forename { get; set; }

        [Display(Name ="Nazwisko:")]
        public string Surname { get; set; }
        
        [Display(Name = "Email:")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Podaj adres email")]
        [EmailAddress(ErrorMessage ="Wpisz właściwy adres email")]
        
        public string Email { get; set; }

        [Display(Name ="Hasło:")]   
        [Required(AllowEmptyStrings =false,ErrorMessage ="Wpisz hasło")]
        [DataType(DataType.Password)]       
        [MinLength(8,ErrorMessage ="Hasło musi posiadać minimum 8 znaków")]
        public string Password { get; set; }

        [Display(Name ="Potwierdź hasło:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Hasło i jego potwierdzenie różnią się")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Ranga:")]
        public string Role { get; set; }

        [ScaffoldColumn(false)] // nie wyświetlane w formularzu
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public bool EmailVerification { get; set; }
        [ScaffoldColumn(false)]
        public System.Guid UniqueCode { get; set; } 
    }

    public class Login
    {
        [Display(Name = "Nazwa użytkonika:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj nazwę użytkonika")]
        [RegularExpression(@"^[a-z0-9A-Z]+$", ErrorMessage = "Pole zawiera niedozwolone znaki.")]       
        public string Nickname { get; set; }

        [Display(Name = "Hasło:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wpisz hasło")]
        [DataType(DataType.Password)]       
        public string Password { get; set; }

    }

    public class ChangePass
    {
        [Display(Name = "Email:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj adres email")]
        [EmailAddress(ErrorMessage = "Wpisz właściwy adres email")]

        public string Email { get; set; }
    }

    public class NewPassword
    {

        [Display(Name = "Nowe hasło:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wpisz hasło")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Hasło musi posiadać minimum 8 znaków")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie różnią się")]
        public string ConfirmPassword { get; set; }

     
    }

}