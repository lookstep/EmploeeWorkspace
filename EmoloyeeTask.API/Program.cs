using EmoloyeeTask.API.Auth;
using EmoloyeeTask.Data;
using EmoloyeeTask.Data.Interfaces;
using EmployeeTask.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidAudience = AuthOptions.Audience,
            RequireSignedTokens = true,
            IssuerSigningKey = AuthOptions.PublicKey
        };
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["token"];
                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});
builder.Services.AddSwaggerGen(c => 
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "EmployeeTask.Shared.xml");
    var sharedFilePath = Path.Combine(AppContext.BaseDirectory, "EmoloyeeTask.API.xml");

    c.IncludeXmlComments(filePath);
    c.IncludeXmlComments(sharedFilePath);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("EmoloyeeTask.Data.Migrations"));
});

builder.Services.AddScoped<IDbRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IDbRepository<Division>,  DivisionRepository>();
builder.Services.AddScoped<IDbRepository<Project> , ProjectRepository>();
builder.Services.AddScoped<IDbRepository<Assignment>, AssignmentRepository>();
builder.Services.AddScoped<IDbRepository<LaborCost>, LaborCostRepository>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseAuthentication();
app.UseAuthorization();


