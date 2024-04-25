using EventSourcingDemo.Persistence;
using EventSourcingDemo.Repository;
using EventSourcingDemo.Service;
using Microsoft.EntityFrameworkCore;
using EventHandler = EventSourcingDemo.Event.EventHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EventSourcingDemoDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("NpgSql"));
},ServiceLifetime.Transient);
builder.Services.AddScoped<EventHandler>();
builder.Services.AddScoped<PokemonReadRepository>();
builder.Services.AddScoped<PokemonRepository>();
builder.Services.AddScoped<PokemonService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
app.Run();