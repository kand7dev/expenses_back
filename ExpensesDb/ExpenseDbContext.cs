﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesDb
{
    public class ExpenseDbContext: DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options): base(options)
        {

        }
        public DbSet<Expense> Expenses { get; set; }

    }
}
