#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FAMS.Api/FAMS.Api.csproj", "FAMS.Api/"]
COPY ["FAMS.Core/FAMS.Core.csproj", "FAMS.Core/"]
COPY ["FAMS.Domain/FAMS.Domain.csproj", "FAMS.Domain/"]
RUN dotnet restore "FAMS.Api/FAMS.Api.csproj"
COPY . .
WORKDIR "/src/FAMS.Api"
RUN dotnet build "FAMS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FAMS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FAMS.Api.dll"]