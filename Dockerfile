FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish

COPY src/Habit.Domain/Habit.Domain.csproj src/Habit.Domain/
COPY src/Habit.Application/Habit.Application.csproj src/Habit.Application/
COPY src/Habit.Infrastructure/Habit.Infrastructure.csproj src/Habit.Infrastructure/
COPY src/Habit.Api/Habit.Api.csproj src/Habit.Api/

RUN dotnet restore src/Habit.Api/Habit.Api.csproj

COPY . .

RUN dotnet publish src/Habit.Api/Habit.Api.csproj -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
EXPOSE 5001

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Habit.Api.dll"]