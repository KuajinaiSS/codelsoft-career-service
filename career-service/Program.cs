using Microsoft.AspNetCore.Server.Kestrel.Core;
using career_service.Extensions;
using career_service.Services;
using MassTransit;
using RabbitMQ.Client;
using SubjectProto;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Puerto para HTTP con HTTP/1.1
    options.ListenAnyIP(5002, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });
    
    // Puerto para gRPC con HTTP/2
    options.ListenAnyIP(5012, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Cache());
});

// Configurar MassTransit a RabiitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.Send<Subject>(config =>
        {
            config.UseRoutingKeyFormatter(context => "subject-queue");
        });
        
    });
});



var app = builder.Build();
app.UseOutputCache();

AppSeedService.SeedDatabase(app);


// Configure the HTTP request pipeline.
app.MapGrpcService<CareerService>();
app.MapGrpcService<SubjectsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();