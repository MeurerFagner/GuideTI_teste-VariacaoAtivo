#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/VariacaoAtivo.API/VariacaoAtivo.API.csproj", "src/VariacaoAtivo.API/"]
COPY ["src/VariacaoAtivo.APP/VariacaoAtivo.APP.csproj", "src/VariacaoAtivo.APP/"]
COPY ["src/VariacaoAtivo.Dominio/VariacaoAtivo.Dominio.csproj", "src/VariacaoAtivo.Dominio/"]
COPY ["src/VariacaoAtivo.Infra/VariacaoAtivo.Infra.csproj", "src/VariacaoAtivo.Infra/"]
RUN dotnet restore "src/VariacaoAtivo.API/VariacaoAtivo.API.csproj"
COPY . .
WORKDIR "/src/src/VariacaoAtivo.API"
RUN dotnet build "VariacaoAtivo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VariacaoAtivo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VariacaoAtivo.API.dll"]