FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sample.Rabbit.Api.csproj", "src/Sample.Rabbit.Api/"]
RUN dotnet restore "src/Sample.Rabbit.Api/Sample.Rabbit.Api.csproj"
COPY . .
WORKDIR "/src/src/Sample.Rabbit.Api"
RUN dotnet build "Sample.Rabbit.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Rabbit.Api.csproj" -c Release -o /app/publish

ENV TZ=America/Sao_Paulo
ENV LANG pt-BR
ENV LANGUAGE pt-BR
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Rabbit.Api.dll"]