FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG username
ARG password
WORKDIR /app
COPY . .

RUN dotnet nuget add source https://ci.appveyor.com/nuget/ekinbulut --name appveyor --username $username --password $password --store-password-in-clear-text
RUN dotnet restore --disable-parallel
RUN  dotnet build -c Release --no-restore -o output


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.12-alpine3.12 AS runtime
WORKDIR /app
COPY --from=build /app/output ./

EXPOSE 8093

ENTRYPOINT ["dotnet", "Library.Authentication.Api.dll"]