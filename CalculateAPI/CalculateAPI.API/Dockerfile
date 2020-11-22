FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["CalculateAPI.API/CalculateAPI.API.csproj", "CalculateAPI/"]
COPY ["CalculateAPI.Service/CalculateAPI.Service.csproj", "CalculateAPI.Service/"]
COPY ["CalcuateAPI.Messaging.Receive/CalcuateAPI.Messaging.Receive.csproj", "CalcuateAPI.Messaging.Receive/"]

RUN dotnet restore "CalculateAPI.API/CalculateAPI.API.csproj"
RUN dotnet restore "CalculateAPI.Service/CalculateAPI.Service.csproj"
RUN dotnet restore "CalcuateAPI.Messaging.Receive/CalcuateAPI.Messaging.Receive.csproj"
COPY . ./

RUN dotnet build "CalculateAPI.API/CalculateAPI.API.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "CalculateAPI.API/CalculateAPI.API.csproj" --no-restore -c Release -o /app/publish

FROM build-env AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculateAPI.dll"]