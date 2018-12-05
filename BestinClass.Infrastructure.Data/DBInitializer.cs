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