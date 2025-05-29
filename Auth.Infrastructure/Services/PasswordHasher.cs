﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Auth.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool VerifyPassword(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
