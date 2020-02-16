FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS greta-food-api-build
WORKDIR /greta-food-api
COPY ./GretaFoodCore/. ./GretaFoodCore/
RUN cd ./GretaFoodCore/ &&\
    dotnet build GretaFoodCore.sln -c Release -o /greta-food-api/bin/release
	
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS greta-food-api-runtime
COPY --from=greta-food-api-build /greta-food-api/bin/release /app
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "GretaFoodCore.Api.dll"]
