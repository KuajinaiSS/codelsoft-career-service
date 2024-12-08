using career_service.Extensions;
using career_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();


builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Cache());
});

var app = builder.Build();
app.UseOutputCache();

AppSeedService.SeedDatabase(app);


// Configure the HTTP request pipeline.
app.MapGrpcService<CareerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();