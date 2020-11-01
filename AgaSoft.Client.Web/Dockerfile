#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 3306

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AgaSoft.Client.Web/AgaSoft.Client.Web.csproj", "AgaSoft.Client.Web/"]
COPY ["AgaSoft.Client.MiddleWares/AgaSoft.Client.MiddleWares.csproj", "AgaSoft.Client.MiddleWares/"]
COPY ["AgaSoft.Client.Interfaces/AgaSoft.Client.Interfaces.csproj", "AgaSoft.Client.Interfaces/"]
COPY ["AgaSoft.Client.Model/AgaSoft.Client.Model.csproj", "AgaSoft.Client.Model/"]
COPY ["AgaSoft.Client.Providers/AgaSoft.Client.Providers.csproj", "AgaSoft.Client.Providers/"]
RUN dotnet restore "AgaSoft.Client.Web/AgaSoft.Client.Web.csproj"
COPY . .
WORKDIR "/src/AgaSoft.Client.Web"
RUN dotnet build "AgaSoft.Client.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgaSoft.Client.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgaSoft.Client.Web.dll"]