using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesCore.DTO
{
    public class AuthenticatedUser
    {
        public required string Token { get; set; }
        public required string Username { get; set; }
    }
}
