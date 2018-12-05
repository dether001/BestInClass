using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Service
{
    public interface IUserService
    {
        //CREATE
        User NewUser(string username, byte[] passwordHash, byte[] passwordSalt, bool isAdmin);
        User CreateUser(User user);
        
        //READ
        List<User> GetAllUsers();
        User GetUserById(int id);

        //UPDATE
        User UpdateUser(User userUpdate);

        //DELETE
        void DeleteUser(int id);


        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        void RegisterUser(string username, string password);
    }
}