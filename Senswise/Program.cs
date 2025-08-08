using Microsoft.OpenApi.Models;
using Senswise.Infra;
using Senswise.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});


Action<SqlDbOptions> dbOptions = (opts) =>
{
    opts.ConnectionString = builder.Configuration["PostgreSql:ConnectionString"];
    opts.DatabaseName = builder.Configuration["PostgreSql:DatabaseName"];
};

builder.Services.Configure(dbOptions);
builder.Services.AddPersistenceServices();
builder.Services.AddScoped<CustomerCommandHandler>();
builder.Services.AddScoped<CustomerQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();
app.MapControllers();
app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}