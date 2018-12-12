using System.Collections.Generic;
using System.IO;
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
            if(user.PasswordHash == null || user.PasswordSalt == null)
                { throw new InvalidDataException("Invalid, user must be registered."); }
            if(user.Username.Length < 1 || user.Username.Length > 25)
                { throw new InvalidDataException("User name must be between 0 and 26 characters."); }
            return _userRepository.CreateUser(user);
        }

        public List<User> GetAllUsers()
        {
            if (_userRepository.ReadAllUsers().Count() < 1)
                { throw new FileNotFoundException("Database is empty."); }
            return _userRepository.ReadAllUsers().ToList();
        }

        public User GetUserById(int id)
        {
            if (_userRepository.GetUserById(id) == null)
                { throw new FileNotFoundException("No match were found."); }
            return _userRepository.GetUserById(id);
        }

        public User UpdateUser(User userUpdate)
        {
            return _userRepository.UpdateUser(userUpdate);
        }

        public void DeleteUser(int id)
        {
            if (_userRepository.GetUserById(id) == null)
                { throw new FileNotFoundException("No match were found."); }
            _userRepository.DeleteUser(id);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if(password == null || password.Length < 5 || password.Length > 25)
                { throw new InvalidDataException("Password must be between 4 and 26 characters."); }
            if (!password.Any(n => char.IsDigit(n)) || !password.Any(n => char.IsLetter(n)))
                { throw new InvalidDataException("Password must contain atleast 1 number and letter."); }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public void RegisterUser(string username, string password)
        {
            if (!password.Any(n => char.IsDigit(n)) || !password.Any(n => char.IsLetter(n)))
                { throw new InvalidDataException("Password must contain atleast 1 number and letter."); }
            if (username.Length < 5 || username.Length > 25)
                { throw new InvalidDataException("Username must be between 4 and 26 characters."); }
            if (password == null || password.Length < 5 || password.Length > 25)
                { throw new InvalidDataException("Password must be between 4 and 26 characters."); }

            byte[] passwordHashReg, passwordSaltReg;
            CreatePasswordHash(password, out passwordHashReg, out passwordSaltReg);
            CreateUser(NewUser(username, passwordHashReg, passwordSaltReg, false));
        }
    }
}