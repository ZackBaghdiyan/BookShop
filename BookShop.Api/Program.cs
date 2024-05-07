using BookShop.Api.Extensions;
using BookShop.Api.Middlewares;
using BookShop.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

var dbOption = builder.Configuration.ConfigureDbOptions();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GlobalExceptionHandler>();
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