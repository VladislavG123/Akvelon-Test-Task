FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Akvelon.TestTask.Web/Akvelon.TestTask.Web.csproj", "Akvelon.TestTask.Web/"]
RUN dotnet restore "Akvelon.TestTask.Web/Akvelon.TestTask.Web.csproj"
COPY . .
WORKDIR "/src/Akvelon.TestTask.Web"
RUN dotnet build "Akvelon.TestTask.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Akvelon.TestTask.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Akvelon.TestTask.Web.dll"]
