FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY DesafioEmpresaCursos.sln .
COPY DesafioEmpresaCursos.API/*.csproj DesafioEmpresaCursos.API/
COPY DesafioEmpresaCursos.Domain/*.csproj DesafioEmpresaCursos.Domain/
COPY DesafioEmpresaCursos.Infra/*.csproj DesafioEmpresaCursos.Infra/

RUN dotnet restore DesafioEmpresaCursos.API/DesafioEmpresaCursos.API.csproj

COPY . .
WORKDIR /src/DesafioEmpresaCursos.API
RUN dotnet build DesafioEmpresaCursos.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish DesafioEmpresaCursos.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "DesafioEmpresaCursos.API.dll"]