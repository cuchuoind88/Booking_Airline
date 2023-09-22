using Booking_Airline.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Booking_Airline.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var tokenWithFullCheck= new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwtconfig").GetValue<string>("SecretKey"))),
    RequireExpirationTime = true,
    ValidAudience = configuration["JWT:ValidAudience"],
    ValidIssuer = configuration["JWT:ValidIssuer"],
    ClockSkew = TimeSpan.Zero
};
var checkRefreshToken= new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:RefreshKey"])),
    RequireExpirationTime = true,
    ValidAudience = configuration["JWT:ValidAudience"],
    ValidIssuer = configuration["JWT:ValidIssuer"],
    ClockSkew = TimeSpan.Zero


};
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection(key: "Jwtconfig"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
)
.AddJwtBearer(options =>
{

    options.TokenValidationParameters = tokenWithFullCheck;
});
builder.Services.AddAutoMapper(typeof(Program));//REGISTER AUTOMAPPER SERVICE
builder.Services.ConfigureRepositoryManager();//REGISTER REPOSITORY MANAGER SERIVICE
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureCors();
builder.Services.AddSingleton(checkRefreshToken);
var app = builder.Build();
app.ConfigureExceptionHandler();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
