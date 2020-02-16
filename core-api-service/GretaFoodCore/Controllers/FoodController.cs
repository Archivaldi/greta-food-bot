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

        
        [HttpGet()]
        public object GetAllFood()
        {
            var result =   _gretaFoodDb.Foods
                .Select(f => new {
                    id = f.Id,
                    name = f.Name,
                    geo = f.Restaurant.GeoTag,
                    restaurantName = f.Restaurant.Name,
                    availableTime = f.AvailableTime,
                    imageUrl = f.ImageUrl
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
    }
}