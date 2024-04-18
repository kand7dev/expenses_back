using ExpensesCore;
using ExpensesDb;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add default services to the container.
builder.Services.AddControllers();

// Add DbContext with connection string
var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ExpenseDbContext>();

// Add ExpensesCore services
builder.Services.AddTransient<IExpensesServices, ExpensesServices>();

// Add Swagger
builder.Services.AddSwaggerDocument(settings =>
{
    settings.Title = "Expenses";
});

// Add Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("ExpensesPolicy", builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("ExpensesPolicy");

app.UseOpenApi();

app.UseSwaggerUi();

app.Run();
