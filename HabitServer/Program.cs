using HabitServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InfrastructureConfigureServices(builder.Configuration);

var app = builder.Build();

app.Run();