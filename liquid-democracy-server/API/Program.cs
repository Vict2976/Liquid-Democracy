using Repository;
using Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HashSettings>(builder.Configuration.GetSection(nameof(HashSettings)));
builder.Services.AddScoped<IHasher, Hasher>();

builder.Services.AddDbContext<LiquidDemocracyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("liquiddemocracy")));
builder.Services.AddScoped<ILiquidDemocracyContext, LiquidDemocracyContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IElectionRepository, ElectionRepository>();
builder.Services.AddScoped<IVotingsRepository, VotingsRepository>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//For Containerizing
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

//Stack Overflow
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials());

app.Run();
