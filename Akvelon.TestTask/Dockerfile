FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Akvelon.TestTask/Akvelon.TestTask.csproj", "Akvelon.TestTask/"]
RUN dotnet restore "Akvelon.TestTask/Akvelon.TestTask.csproj"
COPY . .
WORKDIR "/src/Akvelon.TestTask"
RUN dotnet build "Akvelon.TestTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Akvelon.TestTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Akvelon.TestTask.dll"]
