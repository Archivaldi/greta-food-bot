FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS greta-food-bot-build
WORKDIR /greta-food-bot
COPY ./GretaFoodBot/. ./GretaFoodBot/
RUN cd ./GretaFoodBot/ &&\
    dotnet build GretaFoodBot.sln -c Release -o /greta-food-bot/bin/release
	
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS greta-food-bot-runtime
COPY --from=greta-food-bot-build /greta-food-bot/bin/release /app
WORKDIR /app
ENTRYPOINT ["dotnet", "GretaFoodBot.dll"]