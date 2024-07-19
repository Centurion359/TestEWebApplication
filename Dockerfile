FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TestЕWebApplication1/TestЕWebApplication1.csproj", "TestЕWebApplication1/"]
RUN dotnet restore "TestЕWebApplication1/TestЕWebApplication1.csproj"
COPY . .
WORKDIR "/src/TestЕWebApplication1"
RUN dotnet build "TestЕWebApplication1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestЕWebApplication1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestЕWebApplication1.dll"]