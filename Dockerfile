FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY TrainerBookingSim/WebApi/WebApi.csproj ./TrainerBookingSim/WebApi/
COPY TrainerBookingSim/BusinessLogic/BusinessLogic.csproj ./TrainerBookingSim/BusinessLogic/
COPY TrainerBookingSim/Common/Common.csproj ./TrainerBookingSim/Common/
COPY TrainerBookingSim/DataAccess/DataAccess.csproj ./TrainerBookingSim/DataAccess/
COPY TrainerBookingSim/WebApiTest/WebApiTest.csproj ./TrainerBookingSim/WebApiTest/
COPY TrainerBookingSim/TrainerTest/TrainerTest.csproj ./TrainerBookingSim/TrainerTest/

RUN dotnet restore TrainerBookingSim/WebApi/WebApi.csproj
RUN dotnet restore TrainerBookingSim/BusinessLogic/BusinessLogic.csproj
RUN dotnet restore TrainerBookingSim/Common/Common.csproj
RUN dotnet restore TrainerBookingSim/DataAccess/DataAccess.csproj
RUN dotnet restore TrainerBookingSim/WebApiTest/WebApiTest.csproj
RUN dotnet restore TrainerBookingSim/TrainerTest/TrainerTest.csproj

COPY . ./

RUN dotnet publish TrainerBookingSim/TrainerBookingSim.sln -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "TrainerBookingSim.dll"]
