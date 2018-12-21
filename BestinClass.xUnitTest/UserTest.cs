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

namespace BestinClass.XUnitTest
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
            foreach (var user in userService.GetAllUsers())
            {
                userService.DeleteUser(user.Id);
            }

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
        /*
        [Fact]
        public void Test_UpdateNews()
        {

        }
        */
        #endregion

        #region RegisterUserTests
        [Fact]
        public void TestRegisterUser()
        {
            userService.RegisterUser("gfgfg", "gffg23233");
            foreach (var item in userService.GetAllUsers())
            {
                if (item.Username == "gfgfg")
                {
                    Assert.Contains("gfgfg", item.Username);
                }
            }
        }

        [Fact]
        public void TestRegisterUserExceptions()
        {
            //Password must hold atleast 1 number and letter.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("gfgfg", "gffgh"));
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("gfgfg", "23233"));

            //Password & username must be inbetween 4 and 26 characters.
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("gfgfg", "g2"));
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("gfff", "gffg23233"));

            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("gfgfg", "i234567890i234567890i123456"));
            Assert.Throws<InvalidDataException>(
                () => userService.RegisterUser("i234567890i234567890i123456", "gffg23233"));
        }
        #endregion
    }
}
