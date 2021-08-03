#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyPokedex.Api/MyPokedex.Api.csproj", "MyPokedex.Api/"]
COPY ["MyPokedex.Infrastructure/MyPokedex.Infrastructure.csproj", "MyPokedex.Infrastructure/"]
COPY ["MyPokedex.Core/MyPokedex.Core.csproj", "MyPokedex.Core/"]
COPY ["MyPokedex.Test/MyPokedex.Test.csproj", "MyPokedex.Test/"]
RUN dotnet restore "MyPokedex.Api/MyPokedex.Api.csproj"
COPY . .
WORKDIR "/src/MyPokedex.Api"
RUN dotnet build "MyPokedex.Api.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet test "../MyPokedex.Test/MyPokedex.Test.csproj" -c Release
RUN dotnet publish "MyPokedex.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPokedex.Api.dll"]