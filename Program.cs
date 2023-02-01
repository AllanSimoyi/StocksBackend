using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("http://localhost:4200");
                    });
});
builder.Services.AddDbContext<StockDb>(opt => opt.UseInMemoryDatabase("StockList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);
// builder.Services.UseStartup(builder.WebHost);
// builder.WebHost.UseStartup();
// app.UseCors();

app.MapGet("/stocks", (StockDb db) =>
{
  using (StreamReader r = new StreamReader("data/Stocks.json"))
  {
    string json = r.ReadToEnd();
    if (string.IsNullOrWhiteSpace(json)) return null;

    List<Stock>? stocks = JsonSerializer.Deserialize<List<Stock>>(json);
    if (stocks == null || stocks.Count == 0) return null;

    return stocks;
  }
});

app.MapGet("/stockvalues", (StockDb db) =>
{
  using (StreamReader r = new StreamReader("data/Stock Values.json"))
  {
    string json = r.ReadToEnd();
    if (string.IsNullOrWhiteSpace(json)) return null;

    List<StockValue>? stockValues = JsonSerializer.Deserialize<List<StockValue>>(json);
    if (stockValues == null || stockValues.Count == 0) return null;

    return stockValues;
  }
});

app.Run();