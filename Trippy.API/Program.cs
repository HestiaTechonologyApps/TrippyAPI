using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Trippy.Bussiness;
using Trippy.Domain.Configurations;
using Microsoft.Extensions.Configuration;
using Trippy.InfraCore;
using Trippy.InfraCore;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration as IConfiguration)
    .CreateLogger(); 
// Add services to the container.

builder.Services.AddControllers();

// JWT Authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

CleanOldStdoutLogs();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddMemoryCache();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<OtpSettings>(builder.Configuration.GetSection("OtpSettings"));
builder.Services.Configure<WalletSettings>(builder.Configuration.GetSection("Wallet"));

// Register AppDbContext with SQL Server (adjust as needed)
builder.Services.AddInfraCoreServiceCollectionExtensions(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();
app.UseStaticFiles();
app.UseMiddleware<ExceptionLoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors();
// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
void CleanOldStdoutLogs()
{
    try
    {
        var logFolder = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        if (!Directory.Exists(logFolder))
            return;

        var logFiles = Directory.GetFiles(logFolder, "stdout*");

        foreach (var file in logFiles)
        {
            try
            {
                var info = new FileInfo(file);
                // Delete if older than 7 days
                if (info.CreationTime < DateTime.Now.AddDays(-7))
                {
                    info.Delete();
                }
            }
            catch
            {
                // ignore errors if file is in use
            }
        }
    }
    catch (Exception)
    {

        throw;
    }
   
}
