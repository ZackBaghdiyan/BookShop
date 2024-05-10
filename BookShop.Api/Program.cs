using BookShop.Api.Extensions;
using BookShop.Api.Mapping;
using BookShop.Api.MiddleWares;
using BookShop.Api.Services.Implementations;
using BookShop.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

var dbOption = builder.Configuration.ConfigureDbOptions();
var jwtOption = builder.Configuration.GetJwtOptions();

builder.Services.AddHostedService<DatabaseMigrationService>();
builder.Services.AddSingleton(jwtOption);
builder.Services.AddSingleton(dbOption);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAllServices();
builder.Services.AddBookShopDbContext(dbOption);
builder.Services.ConfigureJwt(jwtOption);
builder.Services.ConfigureSwagger();
builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();