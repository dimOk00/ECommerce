#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ECommerce.API.Customers/ECommerce.API.Customers.csproj", "ECommerce.API.Customers/"]
RUN dotnet restore "ECommerce.API.Customers/ECommerce.API.Customers.csproj"
COPY . .
WORKDIR "/src/ECommerce.API.Customers"
RUN dotnet build "ECommerce.API.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.API.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.Customers.dll"]