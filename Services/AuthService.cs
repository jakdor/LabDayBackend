using System;
using System.Linq;
using System.Security.Cryptography;
using LabDayBackend.Models;
using LabDayBackend.Models.Db;

namespace LabDayBackend.Services
{
    public class AuthService : IAuthService
    {
        public const int SaltSize = 64;
        public const int HashSize = 64;
        public const int Iterations = 10000;

        private LabDayContext _context;

        public AuthService(LabDayContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;

            var user = _context.Users.Where(obj => obj.Username == username).FirstOrDefault();
            if(user == null) return null;
            Console.WriteLine("user");

            if(!VerifyHash(password, user.Hash, user.Salt)) return null;
            Console.WriteLine("win");

            return user;
        }

        public User GetById(int userId)
        {
            var user = _context.Users.Where(obj => obj.Id == userId).FirstOrDefault();
            if(user == null || user.IsBlocked) return null;
            return user;
        }

        public bool Register(string username, string password, int pathId, bool isAdmin)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;

            var userCheck = _context.Users.Where(obj => obj.Username == username).FirstOrDefault();
            if(userCheck != null) return false;

            if(!isAdmin)
            {
                var pathCheck =  _context.Paths.Where(obj => obj.Id == pathId).FirstOrDefault();
                if(pathCheck == null || pathCheck.IsBlocked) return false;
            }

            var saltBytes = CreateSalt();
            var hash = CreateHash(password, saltBytes);

            var user = new User {
                Username = username,
                Hash = hash,
                Salt = saltBytes,
                IsAdmin = isAdmin,
                PathId = pathId
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(int userId)
        {
            var user = _context.Users.Where(obj => obj.Id == userId).FirstOrDefault();
            if(user == null) return false;

            user.IsBlocked = true;

            _context.Update(user);
            _context.SaveChanges();
            return true; 
        }

        private byte[] CreateSalt()
        {
            var provider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltSize];
            provider.GetBytes(salt);
            return salt;
        }

        private byte[] CreateHash(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            return pbkdf2.GetBytes(HashSize);
        }

        private bool VerifyHash(string password, byte[] hash, byte[] salt)
        {
            if (string.IsNullOrEmpty(password)) return false;

            var hashToVerify = CreateHash(password, salt);

            return hashToVerify.SequenceEqual(hash);
        }
    }
}