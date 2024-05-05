using App.Extensions;
using App.Middleware;
using Habits.Endpoints.Extensions;
using Hangfire;
using Users.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.InfrastructureConfigureServices(builder.Configuration);
builder.Services.ApplicationConfigureServices(builder.Configuration);

var app = builder.Build();
app.UseCors(b =>
{
    b.AllowAnyOrigin();
    b.AllowAnyMethod();
    b.AllowAnyHeader();
});

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

await app.HabitModuleInit();
await app.UsersModuleInit();

app.Run();