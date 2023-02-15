using Core;
using Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LiquidDemocracyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("liquiddemocracy")));
builder.Services.AddScoped<ILiquidDemocracyContext, LiquidDemocracyContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/* using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LiquidDemocracyContext>();
    db.Database.Migrate();
} */

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
