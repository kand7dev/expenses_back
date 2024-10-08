﻿using ExpensesCore.CustomExceptions;
using ExpensesCore.DTO;
using ExpensesCore.Utilities;
using ExpensesDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesCore
{
    public class UserService: IUserService
    {
        private readonly ExpenseDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(ExpenseDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (dbUser is null || dbUser.Password is null)
            {
                   throw new InvalidUsernamePasswordException("Invalid username or password");
            }
            if (dbUser.Password is not null && user.Password is not null)
            {
                if (_passwordHasher.VerifyHashedPassword(user, dbUser.Password, user.Password) == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
                {
                    throw new InvalidUsernamePasswordException("Invalid username or password");
                }
            }
            if (user.Username is not null)
            {
                return new AuthenticatedUser
                {
                    Username = user.Username,
                    Token = JwtGenerator.GenerateUserToken(user.Username)
                };
            }
            throw new InvalidUsernamePasswordException("Invalid username or password");
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (checkUser is not null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                if (user is not null && user.Password is not null && user.Username is not null)
                {
                    user.Password = _passwordHasher.HashPassword(user, user.Password);
                }
            }
            await _context!.AddAsync(user!);
            await _context.SaveChangesAsync();
            return new AuthenticatedUser
            {
                Username = user!.Username!,
                Token = JwtGenerator.GenerateUserToken(user.Username!)
            };
 
        }
        public async Task<AuthenticatedUser> ExternalSignIn(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.ExternalId!.Equals(user.ExternalId) && u.ExternalType!.Equals(user.ExternalType));
            if (dbUser is null)
            {
                user.Username = CreateUniqueUsernameFromEmail(user.Email);
                return await SignUp(user);
            }
            return new AuthenticatedUser
            {
                Username = dbUser.Username!,
                Token = JwtGenerator.GenerateUserToken(dbUser.Username!)
            };
        }

        private string? CreateUniqueUsernameFromEmail(string? email)
        {
            var emailSplit = email!.Split("@").First();
            var random = new Random();
            var username = emailSplit;
            while (_context.Users.Any(u => u.Username == username))
            {
                username = emailSplit + random.Next(1000, 9999);
            }
            return username;
        }
    }
}
