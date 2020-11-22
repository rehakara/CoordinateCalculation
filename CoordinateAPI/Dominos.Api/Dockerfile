FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Dominos.Api/Dominos.Api.csproj", "Dominos.Api/"]
COPY ["Dominos.Core/Dominos.Core.csproj", "Dominos.Core/"]
COPY ["Dominos.Data/Dominos.Data.csproj", "Dominos.Data/"]
COPY ["Dominos.Services/Dominos.Services.csproj", "Dominos.Service/"]
COPY ["Dominos.Messaging.Send/Dominos.Messaging.Send.csproj", "Dominos.Messaging.Send/"]
COPY ["Dominos.Tests/Dominos.Tests.csproj", "Dominos.Tests/"]

RUN dotnet restore "Dominos.Api/Dominos.Api.csproj"
RUN dotnet restore "Dominos.Core/Dominos.Core.csproj"
RUN dotnet restore "Dominos.Data/Dominos.Data.csproj"
RUN dotnet restore "Dominos.Services/Dominos.Services.csproj"
RUN dotnet restore "Dominos.Messaging.Send/Dominos.Messaging.Send.csproj"
RUN dotnet restore "Dominos.Tests/Dominos.Tests.csproj"
COPY . ./

RUN dotnet build "Dominos.Api/Dominos.Api.csproj" -c Release -o /app/build --no-restore
RUN dotnet build "Tests/Dominos.Tests/Dominos.Tests.csproj" -c Release --no-restore

FROM build AS test  
ARG BuildId
LABEL test=${BuildId}
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx"  Tests/Dominos.Tests/Dominos.Tests.csproj

FROM build AS publish
RUN dotnet publish "Dominos.Api/Dominos.Api.csproj" --no-restore -c Release -o /app/publish

FROM build-env AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DominosApi.dll"]