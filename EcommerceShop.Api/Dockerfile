#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EcommerceShop.Api/EcommerceShop.Api.csproj", "EcommerceShop.Api/"]
COPY ["EcommerceShop.EF/EcommerceShop.EF.csproj", "EcommerceShop.EF/"]
COPY ["EcommerceShop.Core/EcommerceShop.Core.csproj", "EcommerceShop.Core/"]
RUN dotnet restore "EcommerceShop.Api/EcommerceShop.Api.csproj"
COPY . .
WORKDIR "/src/EcommerceShop.Api"
RUN dotnet build "EcommerceShop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcommerceShop.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceShop.Api.dll"]