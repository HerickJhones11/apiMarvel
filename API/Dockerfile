FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build-env
WORKDIR /app
EXPOSE 80


COPY . ./
RUN dotnet restore
RUN dotnet publish ./API/API.csproj -c Release -o ./API/out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime
WORKDIR /app
COPY --from=build-env /app/API/out ./

ENTRYPOINT ["dotnet", "API.dll"]