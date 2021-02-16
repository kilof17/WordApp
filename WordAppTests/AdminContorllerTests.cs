using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WordApp.Controllers;
using WordApp.DataAccessLayer;
using WordApp.Models;

namespace WordAppTests
{
    [TestFixture]
    public class AdminContorllerTests
    {     
       [Test]
       public void GetAllUsers_IsItNotReturnEmptyModel()
        {
            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.AllUsers()).Returns(GetSapmleUsers());

            var adminContorller = new AdminController(mock.Object);
            var expected = GetSapmleUsers();
            var result = adminContorller.GetAllUsers() as ViewResult;            
                       
            Assert.IsNotNull(result.ViewData.Model);
        }

        private List<User> GetSapmleUsers()
        {
            var allUsers = new List<User>
            {
                new User
                {
                    Id = 0,
                    Nickname = "Marcin",
                    Password = "1234",
                    Role = "GetUser",
                    Surname = "",
                    UniqueCode = Guid.NewGuid()
                },
                new User
                {
                    Id = 1,
                    Nickname = "Magda",
                    Password = "1234",
                    Role = "GetUser",
                    Surname = "",
                    UniqueCode = Guid.NewGuid()
                }
            };
        return allUsers;
        }   
    }
}
