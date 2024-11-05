using Asp.Versioning;
using HealthTrackr_Api.Data;
using HealthTrackr_Api.Repository;
using HealthTrackr_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.
builder.Configuration.AddAzureAppConfiguration(opts =>
{
    opts.Connect(builder.Configuration["AZURE_APP_CONFIGURATION"])
           .Select("*")
           .ConfigureRefresh(refresh =>
           {
               refresh.Register("~REFRESH_ALL", refreshAll: true)
                      .SetCacheExpiration(TimeSpan.FromSeconds(30));
           })
           .UseFeatureFlags(flags => flags.CacheExpirationInterval = TimeSpan.FromSeconds(30));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(opts =>
{
    opts.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

builder.Services.AddAuthentication("Bearer").AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Authentication:SecretKey")))
    };
});

// Repositories
builder.Services.AddScoped<AccessRepository>();
builder.Services.AddScoped<UserRepository>();

// Services
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<UserLoginAccess>();

// Database
builder.Services.AddDbContextFactory<DataContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DB_KEVINS")));
builder.Services.AddAuthorization();

// Bind configuration AppSettings section to the Settings object
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

// Add Feature Management
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement"));

// Add Azure App Configuration middleware to the container of services.
builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();

// Add versioning to the API
builder.Services.AddApiVersioning(opts =>
{
    opts.ReportApiVersions = true;
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new ApiVersion(1, 0);
})
.AddMvc()
.AddApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'VVV";
    opts.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Azure App Configuration middleware for dynamic configuration refresh.
app.UseAzureAppConfiguration();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
