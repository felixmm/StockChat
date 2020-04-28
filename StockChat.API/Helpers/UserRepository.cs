using StockChat.API.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StockChat.API.Helpers
{
    public class UserRepository
    {
        private readonly ChatContext dbContext;

        public UserRepository() : this(new ChatContext())
        { }

        public UserRepository(ChatContext context)
        {
            this.dbContext = context;
        }

        public User Get(string username)
        {
            return this.dbContext.Users.FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));
        }

        public User Get(int id)
        {
            return this.dbContext.Users.FirstOrDefault(u => u.Id.Equals(id));
        }

        public User Register(string username, string password)
        {
            User newUser = null;

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            { 
                return newUser; 
            }

            var existingUser = this.Get(username);
            if(existingUser != null)
            {
                return newUser;
            }

            byte[] hash, salt;

            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            newUser = new User
            {
                Username = username,
                Hash = hash,
                Salt = salt
            };

            this.dbContext.Users.Add(newUser);
            this.dbContext.SaveChanges();

            return newUser;
        }

        public User Login(string username, string password)
        {
            User user = null;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return user;
            }

            user = this.Get(username);
            if(user == null)
            {
                return user;
            }

            if(!VerifyUserPassword(user, password))
            {
                return null;
            }

            return user;
        }

        private bool VerifyUserPassword(User user, string password)
        {
            using (var hmac = new HMACSHA512(user.Salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.Hash[i]) return false;
                }
            }

            return true;
        }
    }
}
