var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Swagger i�in gerekli
builder.Services.AddSwaggerGen(); // Swagger i�in gerekli

//Redis
builder.Services.AddStackExchangeRedisCache(options=>options.Configuration="localhost:1453");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Swagger'� aktif et
    app.UseSwaggerUI(); // Swagger UI'yi aktif et
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
