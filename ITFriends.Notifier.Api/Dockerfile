#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ITFriends.Notifier.Api/ITFriends.Notifier.Api.csproj", "ITFriends.Notifier.Api/"]
COPY ["ITFriends.Infrastructure.SeedWork/ITFriends.Infrastructure.SeedWork.csproj", "ITFriends.Infrastructure.SeedWork/"]
COPY ["ITFriends.Infrastructure.EventBus/ITFriends.Infrastructure.EventBus.csproj", "ITFriends.Infrastructure.EventBus/"]
COPY ["ITFriends.Infrastructure.Configuration/ITFriends.Infrastructure.Configuration.csproj", "ITFriends.Infrastructure.Configuration/"]
COPY ["ITFriends.Infrastructure.Domain.Common/ITFriends.Infrastructure.Domain.Common.csproj", "ITFriends.Infrastructure.Domain.Common/"]
COPY ["ITFriends.Infrastructure.Domain.Write/ITFriends.Infrastructure.Domain.Write.csproj", "ITFriends.Infrastructure.Domain.Write/"]
COPY ["ITFriends.Infrastructure.Data/ITFriends.Infrastructure.Data.csproj", "ITFriends.Infrastructure.Data/"]
COPY ["ITFriends.Infrastructure.Domain.Read/ITFriends.Infrastructure.Domain.Read.csproj", "ITFriends.Infrastructure.Domain.Read/"]
COPY ["ITFriends.Notifier.Core/ITFriends.Notifier.Core.csproj", "ITFriends.Notifier.Core/"]
RUN dotnet restore "ITFriends.Notifier.Api/ITFriends.Notifier.Api.csproj"
COPY . .
WORKDIR "/src/ITFriends.Notifier.Api"
RUN dotnet build "ITFriends.Notifier.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITFriends.Notifier.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITFriends.Notifier.Api.dll"]
