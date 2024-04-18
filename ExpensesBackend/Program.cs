using ExpensesCore;
using ExpensesDb;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add default services to the container.
builder.Services.AddControllers();

// Add DbContext with connection string
var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(connectionString));

// Add ExpensesCore services
builder.Services.AddTransient<IExpensesServices, ExpensesServices>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
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

var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("ExpensesPolicy");

app.UseOpenApi();

app.UseSwaggerUi();

app.Run();
