using BestinClass.Core.Entity;

namespace BestinClass.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(BestinClassContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            
            string password = "1234";
            byte[] passwordHashAdmin, passwordSaltAdmin, passwordHashUser, passwordSaltUser;
            CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);
            CreatePasswordHash(password, out passwordHashUser, out passwordSaltUser);

            var te1 = ctx.TestEntity.Add(new TestEntity()
            {
                
                StringTest = "This a test bruv, nothing too see here...",
                IntTest = 42,
                
            }).Entity;

            var testCar1 = ctx.Car.Add(new Car()
            {
                Make = "BMW",
                Model = "3 series",
                Year = 2008,
                Type = "Sedan",
                Picture = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/2018_BMW_M3_3.0.jpg/1200px-2018_BMW_M3_3.0.jpg"

            }).Entity;

            var testReview = ctx.Review.Add(new Review()
            {
                Header = "Cool daily driver",
                Body = "Used it for a few years, holds up good as new!",
                Car = null,
                RatingEveryday = 5,
                RatingExterior = 4,
                RatingInterior = 3,
                RatingPracticality = 3,
                RatingWeekend = 5
                
            }).Entity;
            
            var userAdmin = ctx.User.Add(new User()
            {
                Username = "Admin",
                PasswordHash = passwordHashAdmin,
                PasswordSalt = passwordSaltAdmin,
                IsAdmin = true
            }).Entity;

            var userUser = ctx.User.Add(new User()
            {
                Username = "User",
                PasswordHash = passwordHashUser,
                PasswordSalt = passwordSaltUser,
                IsAdmin = false
            }).Entity;

            var newNews = ctx.News.Add(new News()
            {
                Header = "Something Important!!!",
                ShortDesc = "this is a short Description of the news segment",
                Picture = "https://hips.hearstapps.com/roa.h-cdn.co/assets/16/29/1280x640/landscape-1469196037-918-drag.jpg?resize=480:*",
                Tags = "lol, lol, lol",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec felis quam, facilisis euismod rutrum mollis, tempus ultrices orci. Nullam hendrerit nibh nec magna condimentum fringilla. Aliquam et felis magna. Duis rutrum placerat risus, vel condimentum dui. Donec ac tincidunt purus. Cras ac dui eget lorem euismod efficitur. Fusce felis felis, volutpat sed est et, pharetra blandit ligula. Praesent rutrum tincidunt metus ut commodo. Mauris tempus finibus nunc at faucibus. Phasellus sed semper metus, eu iaculis erat."
            }).Entity;

            ctx.SaveChanges();
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}