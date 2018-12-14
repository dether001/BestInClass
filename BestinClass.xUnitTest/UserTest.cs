using BestinClass.Core.Application_Service.Impl;
using BestinClass.Infrastructure.Data;
using BestinClass.Infrastructure.Data.Repositories;
using Microsoft.Data.Sqlite;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BestinClass.xUnitTest
{
    public class UserTest : IDisposable
    {
        readonly SqliteConnection connection;
        readonly UserService userService;

        public UserTest()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<BestinClassContext>()
                            .UseSqlite(connection).Options;
            var dbContext = new BestinClassContext(options);
            DBInitializer.SeedDB(dbContext);

            var userRepo = new UserRepository(dbContext);
            userService = new UserService(userRepo);
        }

        public void Dispose()
        {
            connection.Close();
        }

        #region CreateUserTests
        [Fact]
        public void Test_CreateUser()
        {
            var created = userService.CreateUser(userService.NewUser("g", new byte[7], new byte[9], false));
            Assert.Same(created, userService.GetUserById(created.Id));

        }

        [Fact]
        public void Test_CreateUserExceptions()
        {
            Assert.Throws<InvalidDataException>(
                () => userService.CreateUser(userService.NewUser("", new byte[5], new byte[8], false)));
            Assert.Throws<InvalidDataException>(
                () => userService.CreateUser(userService.NewUser("bmkgbmgklbklgbmkglmbkmbugh", new byte[5], new byte[8], false)));
            Assert.Throws<InvalidDataException>(
                () => userService.CreateUser(userService.NewUser("", null, new byte[8], false)));
            Assert.Throws<InvalidDataException>(
                () => userService.CreateUser(userService.NewUser("", new byte[5], null, false)));
        }
        #endregion

        #region GetUserTests
        [Fact]
        public void Test_GetUser()
        {
            var created = userService.CreateUser(userService.NewUser("g", new byte[7], new byte[9], false));
            Assert.Same(created, userService.GetUserById(created.Id));
        }

        [Fact]
        public void Test_GetUserExceptions()
        {
            Assert.Throws<FileNotFoundException>(
                () => userService.GetUserById(-5));
            Assert.Throws<FileNotFoundException>(
                () => userService.GetUserById(99999955));
        }

        [Fact]
        public void Test_GetAllUsers()
        {
            var created = userService.CreateUser(userService.NewUser("f", new byte[9], new byte[6], false));
            Assert.Contains(created, userService.GetAllUsers());
            var created2 = userService.CreateUser(userService.NewUser("f", new byte[9], new byte[6], false));
            Assert.Contains(created2, userService.GetAllUsers());
            var created3 = userService.CreateUser(userService.NewUser("f", new byte[9], new byte[6], false));
            Assert.Contains(created3, userService.GetAllUsers());
        }

        [Fact]
        public void Test_GetAllUsersExceptions()
        {
            //DBIntitalizer users
            userService.DeleteUser(1);
            userService.DeleteUser(2);

            Assert.Throws<FileNotFoundException>(
                () => userService.GetAllUsers());
        }
        #endregion

        #region DeleteUserTests
        [Fact]
        public void Test_DeleteUser()
        {
            var created = userService.CreateUser(
                userService.NewUser("f", new byte[5], new byte[6], false));
            Assert.Contains(created, userService.GetAllUsers());
            userService.DeleteUser(created.Id);
            Assert.Throws<FileNotFoundException>(
                () => userService.GetUserById(created.Id));
            
        }
        #endregion

        #region UpdateNewsTests
        [Fact]
        public void Test_UpdateNews()
        {

        }
        #endregion

        #region RegisterUserTests
        [Fact]
        public void TestRegisterUser()
        {
            userService.RegisterUser("ggfgrd", "8hhtg5644");
            foreach (var user in userService.GetAllUsers())
            {
                if (user.Username == "ggfgrd")
                {
                    Assert.NotNull(userService.GetUserById(user.Id));
                }
            }
        }

        [Fact]
        public void TestRegisterUserException()
        {
            //Password must contain atleast 1 number.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("ertyui", "ertyuiop"));

            //Password must contain atleast 1 letter.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("ertyui", "689485656"));

            //Username must contain atleast 5 characters.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("erty", "6894fgh5656"));

            //Username must contain less than 26 characters.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("ertyhertyhertyhertyhertyhk", "6894fgh5656"));
            
            //Password must contain atleast 8 characters.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("ert5kkpy", "i234567"));

            //Password must contain less than 26 characters.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("ert5kkpy", "i234567890i234567890i23456"));
        }
        #endregion
    }
}
