FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConsoleApp6/ConsoleApp6.csproj", "ConsoleApp6/"]
RUN dotnet restore "ConsoleApp6/ConsoleApp6.csproj"
COPY . .
WORKDIR "/src/ConsoleApp6"
RUN dotnet build "ConsoleApp6.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp6.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp6.dll"]
