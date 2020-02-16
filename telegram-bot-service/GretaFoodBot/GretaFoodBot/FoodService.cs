using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace GretaFoodBot
{
    public class FoodService
    {
        //TODO: Moove to config
        private const string _foodApiUrl = "http://34.66.77.56";
        //TODO: Add as a template for a user
        private const long maximumDistanceInMeters = 10000;
            
        public async Task<List<ClosestFoodDto>> FindClosestFoodAsync(string currentLocation)
        {
            var allFood = await GetAllFoodAsync();
            var closestFoodResult = new List<ClosestFoodDto>(); 
            foreach (var food in allFood)
            {
                var routeResponse = await RouteFoodAsync(currentLocation, food.Geo);
                if (routeResponse.DistanceInMeters <= maximumDistanceInMeters)
                {
                    closestFoodResult.Add(new ClosestFoodDto
                    {
                        Food = food,
                        DistanceInMeters = routeResponse.DistanceInMeters,
                        ArivalTime = routeResponse.ArivalTime
                    });
                }
            }

            return closestFoodResult;
        }
        
        public async Task<RouteResponse> RouteFoodAsync(string currentLocation, string foodLocation)
        {
            var client = new RestClient(_foodApiUrl);
            var request = new RestRequest($"api/Map?from={currentLocation}&to={foodLocation}");
            var response = await client.ExecuteTaskAsync<RouteResponse>(request);

            return response.Data;
        }
        
        public async Task<List<Food>> GetAllFoodAsync()
        {
            var client = new RestClient(_foodApiUrl);
            var request = new RestRequest("api/Food");
            var response = await client.ExecuteTaskAsync<List<Food>>(request);

            return response.Data;
        }
    }
    
    public class RouteResponse
    {
        public long DistanceInMeters { get; set; }
        public DateTime ArivalTime { get; set; }
    }
    
    public class Food
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AvailableTime { get; set; }
        
        public string ImageUrl { get; set; }
        
        public string RestaurantName { get; set; }
        
        public string Geo { get; set; }

    }
}