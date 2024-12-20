using Cadastro.Api.Configuration;
using Cadastro.Cliente.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<CadastroContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadastroConStr"));
});

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddAutoMapperConfig();

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig();

app.Run();
