using ExpensesCore.CustomExceptions;
using ExpensesCore.DTO;
using ExpensesDb;
using Microsoft.AspNet.Identity;
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
        private readonly IPasswordHasher _passwordHasher;

        public UserService(ExpenseDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (dbUser is null || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }
            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = "test token"
            };

        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }
            user.Password = _passwordHasher.HashPassword(user.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = "test token"
            };
        }
    }
}
