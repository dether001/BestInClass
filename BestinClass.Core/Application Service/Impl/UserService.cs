using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public User NewUser(string username, byte[] passwordHash, byte[] passwordSalt, bool isAdmin)
        {
            var user = new User()
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsAdmin = isAdmin,
            };

            return user;
        }

        public User CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.ReadAllUsers().ToList();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public User UpdateUser(User userUpdate)
        {
            return _userRepository.UpdateUser(userUpdate);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public void RegisterUser(string username, string password)
        {
            byte[] passwordHashReg, passwordSaltReg;
            CreatePasswordHash(password, out passwordHashReg, out passwordSaltReg);
            CreateUser(NewUser(username, passwordHashReg, passwordSaltReg, false));
        }
    }
}