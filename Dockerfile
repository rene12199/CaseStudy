# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Api/Api.csproj", "Api/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Application/Application.csproj", "Application/"]
RUN dotnet restore "Api/Api.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build "Api/Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy production settings
COPY ["Api/appsettings.Production.json", "./appsettings.json"]

# Expose the port the app will run on
EXPOSE 5271

# Set environment variables
ENV ASPNETCORE_URLS=http://localhost:5271 
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Api.dll"]