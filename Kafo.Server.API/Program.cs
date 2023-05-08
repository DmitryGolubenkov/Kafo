using System.Globalization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Kafo.Application.Interfaces;
using Kafo.Server.API.Middlewares;
using Kafo.Server.Application.Authorization;
using Kafo.Server.Application.Authorization.Commands;
using Kafo.Server.Application.Authorization.Services;
using Kafo.Server.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// Add services to the container.
#region Localization

var cultureInfo = new CultureInfo("ru-RU");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
CultureInfo.CurrentCulture = cultureInfo;
CultureInfo.CurrentUICulture = cultureInfo;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterAssemblyModules(typeof(Program).Assembly));

builder.Services.AddControllers();

#region Options

builder.Services.AddOptions<AuthOptions>()
    .Configure<IConfiguration>(
        (options, configuration) => configuration.GetSection("Jwt"))
    .BindConfiguration("Jwt");
    

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Swagger Generator to services and configure it
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Kafo API", Version = "v1" });
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

#region Persistence

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateIssuer = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(builder.Configuration["Jwt:SigningKey"])
    };
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<UserInfo>();
builder.Services.AddScoped<IUserInfo>(context => context.GetRequiredService<UserInfo>());
builder.Services.AddTransient<IAccessControl, AccessControl>();

#endregion


var app = builder.Build();

#region Migrate DB

// Persistence - Migrate database
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();

    var createFirstUserCommand = scope.ServiceProvider.GetRequiredService<CreateFirstUserCommand>();
    await createFirstUserCommand.Execute();

    var options  = scope.ServiceProvider.GetRequiredService<IOptions<AuthOptions>>();
}

#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseUserInfoMiddleware();

app.MapControllers();

app.Run();