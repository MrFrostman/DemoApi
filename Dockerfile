FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY ./ ./
#COPY ./.env ./Product.Api/.env
RUN dotnet tool install --global dotnet-ef
#ENV PATH="${PATH}:/root/.dotnet/tools"
#RUN dotnet ef database update --project Product.Infrastructure --startup-project Product.Api
RUN dotnet publish --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY ./.env ./.env
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_PRINT_TELEMETRY_MESSAGE=false
COPY --from=build-env /app/Product.Api/bin/Release/net6.0 .
EXPOSE 80 
ENTRYPOINT ["dotnet", "Product.Api.dll"]

#COPY Product.GraphQL/Product.Api/bin/Release/net6.0/ App/
#WORKDIR /App
#
#ENV DOTNET_EnableDiagnostics=0
#
#RUN dotnet tool install --global dotnet-ef
#
#RUN dotnet publish --configuration Release
#
#ENTRYPOINT ["dotnet", "Product.Api.dll"]