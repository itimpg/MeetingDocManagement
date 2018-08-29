using System;
using System.Collections.Generic;
using MeetingDoc.Api.Models;
using Newtonsoft.Json;
using System.Linq;

namespace MeetingDoc.Api.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            if (_context.Users.Any(x => x.Email == "admin@itec.com"))
            {
                return;
            }

            var admin = new User
            {
                FirstName = "Admin",
                LastName = "iTec",
                Position = "System Admin",
                Email = "admin@itec.com",
                PhoneNo = "",
                Level = UserLevel.Administrator,
                IsActive = true,
                IsRemoved = false,
                CreatedBy = "admin@itec.com",
                CreatedDate = DateTime.Now,
                UpdatedBy = "admin@itec.com",
                UpdatedDate = DateTime.Now
            };

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("P@ssw0rd", out passwordHash, out passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            _context.Users.Add(admin);
            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}