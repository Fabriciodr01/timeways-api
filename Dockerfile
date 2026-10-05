FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Directory.Build.props", "./"]

COPY ["src/TimewaysAPI.Api/TimewaysAPI.Api.csproj", "src/TimewaysAPI.Api/"]
COPY ["src/TimewaysAPI.Application/TimewaysAPI.Application.csproj", "src/TimewaysAPI.Application/"]
COPY ["src/TimewaysAPI.Domain/TimewaysAPI.Domain.csproj", "src/TimewaysAPI.Domain/"]
COPY ["src/TimewaysAPI.Infrastructure/TimewaysAPI.Infrastructure.csproj", "src/TimewaysAPI.Infrastructure/"]

RUN dotnet restore "src/TimewaysAPI.Api/TimewaysAPI.Api.csproj"

COPY . .

RUN dotnet publish "src/TimewaysAPI.Api/TimewaysAPI.Api.csproj" \
    --configuration Release \
    --no-restore \
    --output /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

RUN apt-get update \
    && apt-get install -y --no-install-recommends libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TimewaysAPI.Api.dll"]