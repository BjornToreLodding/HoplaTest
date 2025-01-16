# Bruk en offisiell .NET runtime som base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Byggetrinn
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UsersApi.csproj", "./"]
RUN dotnet restore "./UsersApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "UsersApi.csproj" -c Release -o /app/build

# Publiseringstrinn
FROM build AS publish
RUN dotnet publish "UsersApi.csproj" -c Release -o /app/publish

# Kjøreklar
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersApi.dll"]
