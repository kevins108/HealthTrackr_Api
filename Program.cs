using HealthTrackr_Api.Data;
using HealthTrackr_Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.
builder.Configuration.AddAzureAppConfiguration(options =>
{
	options.Connect(builder.Configuration["AZURE_APP_CONFIGURATION"])
		   .Select("*")
		   .ConfigureRefresh(refresh =>
		   {
			   refresh.Register("~REFRESH_ALL", refreshAll: true)
					  .SetCacheExpiration(TimeSpan.FromSeconds(30));
		   })
		   .UseFeatureFlags(flags => flags.CacheExpirationInterval = TimeSpan.FromSeconds(30));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddScoped<LoginAccess>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AccessRepository>();


// Database
//builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LOCAL_DB_KEVINS")));

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LOCAL_DB_CONNECTION")));

// Bind configuration AppSettings section to the Settings object
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

// Add Feature Management
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement"));

// Add Azure App Configuration middleware to the container of services.
builder.Services.AddAzureAppConfiguration();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
