using System.Text.Json;
using System.Text.Json.Serialization;
using Insurance.Api.Configuration;
using Insurance.Common.Entity;
using Insurance.Service.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddAuthentication(AuthenticationConfiguration.GetJwtAuthenticationOption())
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, AuthenticationConfiguration.GetJwtBearerOptions());

// جهت خاموش کردن ولیدیت انتیتی قبل از ورود به کنترلر  
builder.Services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerOptions.GetSwaggerGeneratorOptions());

builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }); });


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x => { x.ConfigObject.DefaultModelRendering = ModelRendering.Model; });

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

MigrationUtil.Execute(app);

app.Run();