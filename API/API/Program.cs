using API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Cấu hình chuỗi kết nối đến SQL Server
var connectionStrings = builder.Configuration.GetConnectionString("BookConnectionString");
builder.Services.AddDbContext<BookContext>(opt => opt.UseSqlServer(connectionStrings));

// Cấu hình Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm dịch vụ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Áp dụng middleware CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
