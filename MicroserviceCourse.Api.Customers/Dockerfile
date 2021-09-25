#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MicroserviceCourse.Api.Customers/MicroserviceCourse.Api.Customers.csproj", "MicroserviceCourse.Api.Customers/"]
RUN dotnet restore "MicroserviceCourse.Api.Customers/MicroserviceCourse.Api.Customers.csproj"
COPY . .
WORKDIR "/src/MicroserviceCourse.Api.Customers"
RUN dotnet build "MicroserviceCourse.Api.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroserviceCourse.Api.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceCourse.Api.Customers.dll"]
