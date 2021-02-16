namespace WordApp.Models
{
    using System;
    using System.Collections.Generic;

    public partial class User
    {        
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailVerification { get; set; }
        public string Role { get; set; }
        public System.Guid UniqueCode { get; set; }
              
        public virtual ICollection<DBcontrol> DBcontrols { get; set; }
    }
}
