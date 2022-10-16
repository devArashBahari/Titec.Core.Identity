#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV TZ=Asia/Tehran

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR .
COPY ["Src/Endpoints/Titec.Core.Identity.WebApi/Titec.Core.Identity.WebApi.csproj", "Src/Endpoints/Titec.Core.Identity.WebApi/"]
COPY ["Src/Core/Titec.Core.Identity.Application/Titec.Core.Identity.Application.csproj", "Src/Core/Titec.Core.Identity.Application/"]
COPY ["Src/Core/Titec.Core.Identity.Domain/Titec.Core.Identity.Domain.csproj", "Src/Core/Titec.Core.Identity.Domain/"]
COPY ["Src/Core/Titec.Core.Identity.Common/Titec.Core.Identity.Common.csproj", "Src/Core/Titec.Core.Identity.Common/"]
COPY ["Src/Infrastructure/Titec.Core.Identity.Service/Titec.Core.Identity.Service.csproj", "Src/Infrastructure/Titec.Core.Identity.Service/"]
COPY ["Src/Infrastructure/Titec.Core.Identity.Repository/Titec.Core.Identity.Repository.csproj", "Src/Infrastructure/Titec.Core.Identity.Repository/"]
COPY ["Src/Infrastructure/Titec.Core.Identity.EF/Titec.Core.Identity.EF.csproj", "Src/Infrastructure/Titec.Core.Identity.EF/"]

COPY ["Packages", ".nuget/"]

RUN dotnet restore "Src/Endpoints/Titec.Core.Identity.WebApi/Titec.Core.Identity.WebApi.csproj" -s ./../../../.nuget -s https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/Src/Endpoints/Titec.Core.Identity.WebApi"
RUN dotnet build "Titec.Core.Identity.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Titec.Core.Identity.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Titec.Core.Identity.WebApi.dll"]