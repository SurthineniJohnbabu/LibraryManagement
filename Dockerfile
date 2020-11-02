#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY ["LibraryManagement.API/LibraryManagement.API.csproj", "LibraryManagement.API/"]
COPY ["LibraryManagement.API.BusinessLogic/LibraryManagement.API.BusinessLogic.csproj", "LibraryManagement.API.BusinessLogic/"]
COPY ["LibraryManagement.API.Core/LibraryManagement.API.Core.csproj", "LibraryManagement.API.Core/"]
COPY ["LibraryManagement.API.DataAccess/LibraryManagement.API.DataAccess.csproj", "LibraryManagement.API.DataAccess/"]
COPY ["LibraryManagement.API.Extensions/LibraryManagement.API.Extensions.csproj", "LibraryManagement.API.Extensions/"]
RUN dotnet restore

COPY . .
WORKDIR "/src/LibraryManagement.API"
RUN dotnet build "LibraryManagement.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LibraryManagement.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LibraryManagement.API.dll"]s