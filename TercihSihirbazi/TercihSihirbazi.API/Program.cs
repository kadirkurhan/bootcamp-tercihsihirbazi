using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TercihSihirbazi.API;
using TercihSihirbazi.Business.Concrete;
using TercihSihirbazi.Business.Containers.MicrosoftIoc;
using TercihSihirbazi.Business.Interfaces;
using TercihSihirbazi.Business.StringInfos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost", "http://localhost:8080").AllowAnyHeader()
                                                  .AllowAnyMethod();
        });
});
builder.Services.AddDependencies();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = JwtInfo.Issuer,
        ValidAudience = JwtInfo.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


var app = builder.Build();

//JwtIdentityInitializer initializer = new JwtIdentityInitializer();
//await initializer.Seed();
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;

//var appUserService = builder.Configuration.Get<AppUserManager>();
//var appRoleService = builder.Configuration.Get<AppRoleManager>();
//var appUserRoleService = builder.Configuration.Get<AppUserRoleManager>();

//var appUserService = new ServiceCollection()
//  .AddScoped<IAppUserService>()
//  .BuildServiceProvider();
//var appUserRoleService = new ServiceCollection()
//  .AddScoped<IAppUserRoleService>()
//  .BuildServiceProvider();
//var appRoleService = new ServiceCollection()
//  .AddScoped<IAppRoleService>()
//  .BuildServiceProvider();

void SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using (var scope = app.Services.CreateScope())
    {
        var appUserService = scope.ServiceProvider.GetRequiredService<IAppUserService>();
        var appUserRoleService = scope.ServiceProvider.GetRequiredService<IAppUserRoleService>();
        var appRoleService = scope.ServiceProvider.GetRequiredService<IAppRoleService>();

        JwtIdentityInitializer.Seed(appUserService, appUserRoleService, appRoleService);

    }
}
SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            // c.SwaggerEndpoint("/swagger/index.html", "V1 Docs");
        }
        );
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
