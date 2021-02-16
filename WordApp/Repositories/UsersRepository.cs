using System;
using System.Collections.Generic;
using System.Linq;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public class UsersRepository : IUsersRepository
    {
        private readonly WordAppEntities _context;

        public UsersRepository( WordAppEntities context )
        {
            _context = context;
        }
      
        public List<User> AllUsers()
        {
            var allUsers = _context.Users.ToList();
            return allUsers;
        }

        public User GetUser( int? id )
        {
            var user = _context.Users.Find( id ); 
            return user;
        }

        public User GetUser( string nickname)
        {
            var user = _context.Users.Where(x => x.Nickname == nickname).FirstOrDefault();
            return user;
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public bool EditUser( User editUser )
        {
            var user = _context.Users.Where(p => p.Nickname == editUser.Nickname).FirstOrDefault();
            if (user != null)
            {
                _context.Configuration.ValidateOnSaveEnabled = false; // turns off validation
                user = editUser;           
                _context.SaveChanges();
                return true;
            }
            return false;        
        }

        public bool DeleteUser( int? ID ) 
        {            
            var deleteUser = _context.Users.Find( ID );
            if ( deleteUser != null )
            {
                try
                {
                    var userControls = _context.DBcontrols.Where( a => a.Users.Id == ID );
                    _context.DBcontrols.RemoveRange(userControls);
                    _context.Users.Remove( deleteUser );
                    _context.SaveChanges();                                       
                    return true;
                }
                catch
                {
                    return false;
                }               
            }
            return false;           
        }

        public string AddNewUser(User newUser)
        {        
            _context.Users.Add(newUser);
            _context.SaveChanges();

            SendEmail email = new SendEmail();
            email.VerificationEmail(newUser.Email, newUser.UniqueCode.ToString());

            return "Sukces!! Twoje konto zostało utowrzone. Link aktywacyjny został wysłany na adres: " + newUser.Email;
        }

        public bool Verification(string id)
        {           
            _context.Configuration.ValidateOnSaveEnabled = false;
            var findActivationCodeOwner = _context.Users.Where(user => user.UniqueCode == new Guid(id)).FirstOrDefault();
            if (findActivationCodeOwner != null)
            {
                findActivationCodeOwner.EmailVerification = true;
                _context.SaveChanges();
                return true;
            }
            return false;
           
        }

     
    }
}