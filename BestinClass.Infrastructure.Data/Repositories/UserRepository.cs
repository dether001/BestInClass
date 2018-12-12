using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BestinClass.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        
        private readonly BestinClassContext _ctx;

        public UserRepository(BestinClassContext ctx)
        {
            _ctx = ctx;
        }
        
        public User CreateUser(User user)
        {
            _ctx.Attach(user).State = EntityState.Added;
            _ctx.SaveChanges();
            return user;
        }

        public IEnumerable<User> ReadAllUsers()
        {
            return _ctx.User;
        }

        public User GetUserById(int id)
        {
            return _ctx.User
                .FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(User userUpdate)
        {
            _ctx.Attach(userUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return userUpdate;
        }

        public User DeleteUser(int id)
        {
            var removed = _ctx.User.FirstOrDefault(c => c.Id == id);
            _ctx.Remove(removed);
            _ctx.SaveChanges();
            return removed;
        }
    }
}