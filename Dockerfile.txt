FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj", "src/AI-Ecommerce.Api/"]
COPY ["src/AI-Ecommerce.Data/AI-Ecommerce.Data.csproj", "src/AI-Ecommerce.Data/"]
COPY ["src/AI-Ecommerce.Agent/AI-Ecommerce.Agent.csproj", "src/AI-Ecommerce.Agent/"]

RUN dotnet restore "src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj"

COPY . .
WORKDIR "/src/src/AI-Ecommerce.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AI-Ecommerce.Api.dll"]