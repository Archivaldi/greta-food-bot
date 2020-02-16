using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GretaFoodCore.Api.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GretaFoodCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController
    {
        private GretaFoodDbContext _gretaFoodDb;

        public FoodController(GretaFoodDbContext gretaFoodDb)
        {
            _gretaFoodDb = gretaFoodDb;
        }

        [HttpGet("{restaurantId}")]
        public object GetAllRestaurantFood(string restaurantId)
        {
            var restFood = _gretaFoodDb
                .Foods
                .Where(f => f.RestaurantId.Equals(restaurantId, StringComparison.OrdinalIgnoreCase));
            
            var result =   
                restFood
                .Select(f => new {
                    id = f.Id,
                    name = f.Name,
                    geo = f.Restaurant.GeoTag,
                    restaurantName = f.Restaurant.Name,
                    restaurantAddress = f.Restaurant.Address,
                    karmaScore = f.Restaurant.KarmaScore,
                    availableTime = f.AvailableTime,
                    imageUrl = f.ImageUrl,
                    restrauntId = f.RestaurantId
                })
                .ToList();
            
            return result;
        }
        
        [HttpGet()]
        public object GetAllFood()
        {
            var result =   _gretaFoodDb.Foods
                .Select(f => new {
                    id = f.Id,
                    name = f.Name,
                    geo = f.Restaurant.GeoTag,
                    restaurantName = f.Restaurant.Name,
                    restaurantAddress = f.Restaurant.Address,
                    karmaScore = f.Restaurant.KarmaScore,
                    availableTime = f.AvailableTime,
                    imageUrl = f.ImageUrl,
                    restrauntId = f.RestaurantId
                })
                .ToList();
            return result;
        }
        
        [HttpPost()]
        public async Task<FoodEntity> SaveFood(FoodEntity foodEntity)
        {
            if (foodEntity == null)
            {
                throw new  ApplicationException("Unable to parse input entity");
            }
            foodEntity.Id = Guid.NewGuid().ToString();

            _gretaFoodDb.Foods.Add(foodEntity);
            await _gretaFoodDb.SaveChangesAsync();
            
            return foodEntity;
        }
        
        [HttpPost("{foodEntityId}")]
        public async Task DeleteFoodEntry(string foodEntityId)
        {
            var foodToDelete = _gretaFoodDb.Foods
                .FirstOrDefault(f => f.Id.Equals(foodEntityId));

            if (foodToDelete == null)
            {
                throw new  ApplicationException("Unable to find food to delete");
            }

            _gretaFoodDb.Foods.Remove(foodToDelete);
            await _gretaFoodDb.SaveChangesAsync();
        }
    }
}