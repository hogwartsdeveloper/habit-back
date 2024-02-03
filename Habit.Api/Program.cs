using Habit.Api.Extensions;
using Habit.Api.Middleware;
using Habit.Application.Extensions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InfrastructureConfigureServices(builder.Configuration);
builder.Services.ApplicationConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandleMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHangfireDashboard();
app.AddBackgroundJobs();
app.Run();