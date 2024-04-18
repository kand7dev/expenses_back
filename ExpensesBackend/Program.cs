using ExpensesDb;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ExpenseDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
