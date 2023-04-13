using AutoMapper.Internal;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using SBEU.Gramm.Api.Hubs;
using SBEU.Gramm.Api.Models;
using SBEU.Gramm.Api.Service;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Middleware.Repositories;
using SBEU.Gramm.Middleware.Repositories.Interfaces;

using Serilog;
using Serilog.Events;
using Serilog.Filters;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<InitProfile>();
    cfg.Internal().MethodMappingEnabled = false;
});

var name = Environment.GetEnvironmentVariable("DATABASENAME");
var user = Environment.GetEnvironmentVariable("DATABASEUSER");
var password = Environment.GetEnvironmentVariable("DATABASEPASSWORD");
var host = Environment.GetEnvironmentVariable("DATABASEHOST");
var port = Environment.GetEnvironmentVariable("DATABASEPORT");
var connectionString = $"User ID={user};Password={password};Host={host};Port={port};Database={name};";
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseLazyLoadingProxies().UseNpgsql(connectionString));

var tempbuilder = new DbContextOptionsBuilder<ApiDbContext>();
tempbuilder.UseNpgsql(connectionString);
var tempdb = new ApiDbContext(tempbuilder.Options);
tempdb.Database.Migrate();

builder.Services.Configure<JwtConfig>(config =>
{
    config.ExpiryTimeFrame = TimeSpan.Parse(Environment.GetEnvironmentVariable("EXPIRYTIMEFRAME"));
    config.Secret = Environment.GetEnvironmentVariable("SECRETJWT");
});
var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRETJWT"));
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    RequireExpirationTime = false,

    // Allow to use seconds for expiration of token
    // Required only when token lifetime less than 5 minutes
    // THIS ONE-+
    ClockSkew = TimeSpan.Zero
};
builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameters;
});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue; // if don't set default value is: 30 MB
});

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.UseInlineDefinitionsForEnums();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    options.AddPolicy("local", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost");
    });
});
builder.Services.AddDefaultIdentity<XIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApiDbContext>();

builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration().WriteTo.Logger(x=>x
        .Filter.ByIncludingOnly(s=>s.Level == LogEventLevel.Error || s.Level == LogEventLevel.Fatal)
        .WriteTo.File("logs/errors-.log",rollingInterval:RollingInterval.Day,shared:true))
    .WriteTo.Logger(x=>x
        .Filter.ByIncludingOnly(Matching.FromSource("SBEU.Gramm.Middleware.Repositories"))
        .Filter.ByExcluding(s=>s.Level == LogEventLevel.Fatal)
        .WriteTo.File("logs/repos-.log",rollingInterval:RollingInterval.Day,shared:true))
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddSingleton(Log.Logger);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INPostRepository, NPostRepository>();
builder.Services.AddScoped<INLikeRepository, NLikeRepository>();
builder.Services.AddScoped<INCommentaryRepository,NCommentaryRepository>();
builder.Services.AddScoped<INStoryRepository,NStoryRepository>();
var app = builder.Build();
Console.WriteLine(@"Listening on http://0.0.0.0:51722");
app.Urls.Add("http://0.0.0.0:51722");
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseCors();

app.UseAuthorization();
app.UseAuthentication();
//app.UseEndpoints(end =>{
//    end.MapControllers();
//    end.MapHub<NotificationHub>("/notification");
//});
//app.MapHub<NotificationHub>("/notification");
app.MapControllers();
if (!Directory.Exists("Contents"))
{
    Directory.CreateDirectory("Contents");
}

app.Run();
