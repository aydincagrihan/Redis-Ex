var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Redis ��in ilk olarak cache servisini ekliyoruz
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
/*
1.AddMemoryCache servisi uygulamaya eklenir
2.InMemoryCache referans�n� inject edilir
3.Set metoduyla veriyi cache'leyebilir,Get metoduyla cache'lenmi� veriyi elde edebiliriz
4.Remove fonksiyonu ile cache'lenmi� veriyi silebiliriz
5.TryGetValue metodu ile kontrollu b�r sek�lde cache'den ver�y� okuyabiliriz
*/
