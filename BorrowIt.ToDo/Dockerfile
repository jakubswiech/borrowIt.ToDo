FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./

# Copy csproj and restore as distinct layers
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.ToDo/BorrowIt.ToDo.csproj
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.ToDo.Application/BorrowIt.ToDo.Application.csproj
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.ToDo.Domain/BorrowIt.ToDo.Domain.csproj
RUN dotnet restore --configfile ./nuget.config ./BorrowIt.ToDo.Infrastructure/BorrowIt.ToDo.Infrastructure.csproj
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BorrowIt.ToDo.dll"]