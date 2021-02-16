using System.Collections.Generic;
using WordApp.Models;

namespace WordApp.DataAccessLayer
{
    public interface IUsersRepository
    {
        List<User> AllUsers();
        User GetUser(int? id);
        User GetUser(string nickname);
        User GetUserByEmail(string email);
        bool EditUser(User u);
        bool DeleteUser(int? ID);
        string AddNewUser(User newUser);     
        bool Verification(string id);
    }
}
