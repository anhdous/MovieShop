using System.Text;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieShopAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

builder.Services.AddHttpContextAccessor();

// inject the connection string from appsettings.json into MovieShopDbContext class
builder.Services.AddDbContext<MovieShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection")));

// This is used for checking the incoming token
// API is gonna use JWT authentication so that it can look at the incoming request
// and look for Token and if valid it will get the claims in the HttpContext
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        // The way we create the token is using the secret key so the way we valid it is the same, using the same secret key
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["secretKey"]))
        
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
// When you get a http request from client/browser
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app is WebApplication which is implementing IApplicationBuilder interface, that's why it can use this custom middleware
app.UserMovieShopExceptionMiddleware();
app.UseHttpsRedirection();

app.UseCors(policy =>
{
    policy.WithOrigins(builder.Configuration["AngularURl"]).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
});

//Make sure you add Authentication Middleware, this is used for our Filter
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();