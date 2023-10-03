using System.Text;
using BusinessLogic.Mapping;
using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.Genre;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using BusinessLogic.Services.Implemetations;
using BusinessLogic.Validators;
using DataLayer.DbContext;
using DataLayer.Repositories;
using DataLayer.Repositories.Implementations;
using FluentValidation;
using LibraryAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using LibraryAPI.Extensions;
using LibraryAPI.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddAuthentication(x =>
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
        x.Events = new JwtBearerEvents();
        x.Events.OnMessageReceived = context => {

            if (context.Request.Cookies.ContainsKey("Authorization"))
            {
                context.Token = context.Request.Cookies["Authorization"];
            }

            return Task.CompletedTask;
        };
    });




// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationExtension>();
var connectionString = builder.Configuration.GetConnectionString("CarSharingDb");
builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(connectionString));
ConfigureServices(builder.Services);

var app = builder.Build();


//Configure(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void Configure(WebApplication app)
{
    app.UseMiddleware<LoggingMiddleware>();
    app.UseMiddleware<ExceptionMiddleware>();
}
void ConfigureServices(IServiceCollection serviceCollection)
{
    serviceCollection.AddScoped<IValidator<UserDto>, UserValidator>();
    serviceCollection.AddScoped<IValidator<BookDto>, BookValidator>();
    serviceCollection.AddScoped<IValidator<AuthorDto>, AuthorValidator>();
    serviceCollection.AddScoped<IValidator<GenreDto>, GenreValidator>();
    //
    serviceCollection.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
    serviceCollection.AddScoped<IBookGenreRepository, BookGenreRepository>();
    
    serviceCollection.AddScoped<IUserRepository, UserRepository>();
    serviceCollection.AddScoped<IUserService, UserService>();

    serviceCollection.AddScoped<IBookRepository, BookRepository>();
    serviceCollection.AddScoped<IBookService, BookService>();
    
    serviceCollection.AddScoped<IAuthorRepository, AuthorRepository>();
    serviceCollection.AddScoped<IAuthorService, AuthorService>();
    
    serviceCollection.AddScoped<IGenreRepository, GenreRepository>();
    serviceCollection.AddScoped<IGenreService, GenreService>();
    
    serviceCollection.AddScoped<IBookLoanRepository, BookLoanRepository>();
    serviceCollection.AddScoped<IBookLoanService, BookLoanService>();

    serviceCollection.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    serviceCollection.AddScoped<ITokenService, TokenService>();
    
    serviceCollection.AddAutoMapper(typeof(MappingProfile));
    serviceCollection.AddAutoMapper(typeof(MappingProfileApi));
    
    serviceCollection.AddScoped<IValidator<UserDto>, UserValidator>();
    serviceCollection.AddScoped<IValidator<BookDto>, BookValidator>();
    serviceCollection.AddScoped<IValidator<AuthorDto>, AuthorValidator>();
    serviceCollection.AddScoped<IValidator<GenreDto>, GenreValidator>();
    serviceCollection.AddScoped<IValidator<BookLoanDto>, BookLoanValidator>();
    
}