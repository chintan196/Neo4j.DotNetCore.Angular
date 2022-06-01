#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotnetCore.Neo4j.Angular.Api/DotnetCore.Neo4j.Angular.Api.csproj", "DotnetCore.Neo4j.Angular.Api/"]
RUN dotnet restore "DotnetCore.Neo4j.Angular.Api/DotnetCore.Neo4j.Angular.Api.csproj"
COPY . .
WORKDIR "/src/DotnetCore.Neo4j.Angular.Api"
RUN dotnet build "DotnetCore.Neo4j.Angular.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetCore.Neo4j.Angular.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetCore.Neo4j.Angular.Api.dll"]