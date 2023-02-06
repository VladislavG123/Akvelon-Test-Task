FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Akvelon.TestTask.Contracts/Akvelon.TestTask.Contracts.csproj", "Akvelon.TestTask.Contracts/"]
COPY ["Akvelon.TestTask.DAL/Akvelon.TestTask.DAL.csproj", "Akvelon.TestTask.DAL/"]
COPY ["Akvelon.TestTask.Web/Akvelon.TestTask.Web.csproj", "Akvelon.TestTask.Web/"]
RUN dotnet restore "Akvelon.TestTask.Web/Akvelon.TestTask.Web.csproj"
COPY . .
WORKDIR "/src/Akvelon.TestTask.Web"
RUN dotnet build "Akvelon.TestTask.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Akvelon.TestTask.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["Akvelon.TestTask.Web/nginx.conf", "/etc/nginx/nginx.conf"]