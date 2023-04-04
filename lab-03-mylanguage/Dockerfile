FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /source

COPY lab-03-mylanguage/lab-03-mylanguage.csproj lab-03-mylanguage.csproj

RUN dotnet restore lab-03-mylanguage.csproj

COPY ./lab-03-mylanguage .

RUN dotnet build lab-03-mylanguage.csproj -c Release -o /app/build

RUN dotnet publish lab-03-mylanguage.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

CMD ["dotnet", "lab-03-mylanguage.dll", "serve"]