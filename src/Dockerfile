FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish

COPY Habit.Domain/Habit.Domain.csproj Habit.Domain/
COPY Habit.Application/Habit.Application.csproj Habit.Application/
COPY Habit.Infrastructure/Habit.Infrastructure.csproj Habit.Infrastructure/
COPY Habit.Api/Habit.Api.csproj Habit.Api/

RUN dotnet restore Habit.Api/Habit.Api.csproj

COPY . .

RUN dotnet publish Habit.Api/Habit.Api.csproj -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
EXPOSE 5001

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Habit.Api.dll"]