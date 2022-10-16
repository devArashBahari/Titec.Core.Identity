using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Common.Security;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Core.Identity.Repository.RepositoryAggregates.AppAggregate;
using Titec.Core.Identity.Repository.RepositoryAggregates.PermissionAggregate;
using Titec.Core.Identity.Repository.RepositoryAggregates.RoleAggregate;
using Titec.Core.Identity.Repository.RepositoryAggregates.RolePermissionAggregate;
using Titec.Core.Identity.Repository.RepositoryAggregates.UserAggregate;
using Titec.Core.Identity.Repository.RepositoryAggregates.UserAppAggregate;
using Titec.Core.Identity.Repository.RepositoryAggregates.UserCustomerAggregate;
using Titec.Core.Identity.Repository.RepositoryAggregates.UserRoleAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.AppAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.AppUserAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.PermissionAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.RoleAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.RolePermissionAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.UserAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.UserCustomerAggregate;
using Titec.Core.Identity.Service.ServiceAggregate.UserRoleAggregate;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<BaseDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
/*builder.Services.AddScoped<BaseDbContext>(s => new BaseDbContext(builder.Configuration.GetConnectionString("DefaultConnection")))*/
;

#region AppSettingsStatics
var login = builder.Configuration["LoginSettings:loginKey"];
var loginOTP = builder.Configuration["LoginSettings:loginOTP"];
#endregion
#region IOC
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUserCustomerRepository, UserCustomerRepository>();
builder.Services.AddScoped<IUserCustomerService, UserCustomerService>();

builder.Services.AddScoped<IAppRepository, AppRepository>();
builder.Services.AddScoped<IAppService, AppService>();

builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IAppUserRepository, UserAppRepository>();

builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<ITitecIdentity, TitecIdentity>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<BaseDbContext>>();


builder.Services.AddHttpContextAccessor();
//builder.Services.AddOptions();


#endregion
#region Authentication


builder. Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Titec.Core.Bearer")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion
#region CORS

builder. Services.AddCors(options =>
{
    options.AddPolicy("EnableCors", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build();
    });
});

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Base API",
        Description = "Identity API Swagger Surface",
    });

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed(origin => true)
                   .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();