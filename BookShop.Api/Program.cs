using BookShop.Api.Extensions;
using BookShop.Api.Middlewares;
using BookShop.Api.Services;
using BookShop.Data.Extensions;
using BookShop.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

var dbOption = builder.Configuration.ConfigureDbOptions();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddAllServices();
builder.Services.AddHostedService<DatabaseMigrationService>();
builder.Services.AddBookShopDbContext(dbOption);

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