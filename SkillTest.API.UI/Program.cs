
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkillTest.API.UI.Extentions;
using SkillTest.Core.Application.Services;
using SkillTest.Core.Infrastructures.Data;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SkillTestDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SkillTestDB"));
});

builder.Services.AddRepository();
builder.Services.AddInjection();
// Cors
builder.Services.AddCusumCors(builder.Configuration);
// JWT
builder.Services.AddCustomJWT(builder.Configuration);
builder.Services.addCustomOptions(builder.Configuration);

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Cors
app.UseRouting();
app.UseCors(SecurityMethods.DEFAULT_POLICY);

app.UseAuthorization();

app.MapControllers();

app.Run();
