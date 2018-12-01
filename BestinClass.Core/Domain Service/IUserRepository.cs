using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Domain_Service
{
    public interface IUserRepository
    {
        //CREATE
        User CreateUser(User user);
        //READ
        IEnumerable<User> ReadAllUsers();
        User GetUserById(int id);
        //UPDATE
        User UpdateUser(User userUpdate);
        //DELETE
        User DeleteUser(int id);
    }
}