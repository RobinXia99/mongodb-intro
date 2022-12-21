using MongoExample.Models;
using MongoExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
// Here we register our services which will be used by our controllers.
builder.Services.AddSingleton<PokemonService>();
builder.Services.AddSingleton<TrainerService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// This was needed to enable annotations for our swagger console.
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

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
