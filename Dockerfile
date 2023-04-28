# Use Microsoft's official build .NET image.
# https://hub.docker.com/_/microsoft-dotnet-core-sdk/
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS sdk
WORKDIR /app

# Copy local code to the container image.
COPY ./ ./

# Build a release artifact.
RUN dotnet publish -c Release -o out

# Use Microsoft's official runtime .NET image.
# https://hub.docker.com/_/microsoft-dotnet-core-aspnet/
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=sdk /app/out .

# Make sure the app binds to port 8080
ENV ASPNETCORE_URLS http://*:8080

# Run the web service on container startup.
ENTRYPOINT ["dotnet", "myapp-backend.dll"]
