#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
#WORKDIR /src
#COPY ["CampaignService/CampaignService.csproj", "CampaignService/"]
#
#RUN dotnet restore "CampaignService/CampaignService.csproj"
#COPY . .
#WORKDIR "/src/CampaignService"
#RUN dotnet build "CampaignService.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "CampaignService.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "CampaignService.dll"]


FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CampaignService.dll"]