using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesDb
{
    public class User
    {
        public int Id { get; set; }
        public required string Username{ get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }

    }
}
