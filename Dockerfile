FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish

COPY src/building-blocks src/building-blocks
COPY src/modules src/modules
COPY src/App/App.csproj src/App/

RUN dotnet restore src/App/App.csproj

COPY . .

RUN dotnet publish src/App/App.csproj -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
EXPOSE 5001

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "App.dll"]