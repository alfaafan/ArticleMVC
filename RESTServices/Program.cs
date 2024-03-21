using FluentValidation;
using RESTServices.BLL;
using RESTServices.BLL.Interfaces;
using RESTServices.Data.Interfaces;
using RESTServices.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using RESTServices.BLL.DTOs.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//register DI
builder.Services.AddScoped<ICategoryBLL, CategoryBLL>();
builder.Services.AddScoped<ICategoryData, CategoryData>();
builder.Services.AddScoped<IArticleBLL, ArticleBLL>();
builder.Services.AddScoped<IArticleData, ArticleData>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LatihanDbConnectionString")));


//builder.Services.AddScoped<IRoleBLL, RoleBLL>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssemblyContaining<CategoryCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryUpdateValidator>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
