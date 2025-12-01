# Build stage: restore, build, and publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj first to leverage Docker layer caching for restores
COPY ["CalculadoraKW.Api.csproj", "./"]
RUN dotnet restore "CalculadoraKW.Api.csproj"

# Copy the rest of the files and publish
COPY . .
RUN dotnet publish "CalculadoraKW.Api.csproj" -c Release -o /app/publish

# Runtime stage: copy published app into a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Configure ASP.NET Core to listen on port 80
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CalculadoraKW.Api.dll"]
