using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algorand;
using GretaFoodCore.Api.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GretaFoodCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController
    {
        private GretaFoodDbContext _gretaFoodDb;

        public RestaurantsController(GretaFoodDbContext gretaFoodDb)
        {
            _gretaFoodDb = gretaFoodDb;
        }

        [HttpGet()]
        public List<RestaurantEntity> GetAllRestaurants()
        {
            return  _gretaFoodDb.Restaurants.Include(r => r.FoodEntities)
                .Select(r => new RestaurantEntity
                {
                    Address = r.Address,
                    Id = r.Id,
                    Name =  r.Name,
                    GeoTag = r.GeoTag,
                    AlgorandAddress = r.AlgorandAddress,
                    KarmaScore = r.KarmaScore,
                    FoodEntities = new List<FoodEntity>(r.FoodEntities)
                }).ToList();
        }
        
        [HttpPost()]
        public async Task<RestaurantEntity> SaveRestaurant(RestaurantEntity restaurantEntity)
        {
            if (restaurantEntity == null)
            {
                throw new  ApplicationException("Unable to parse input entity");
            }
            restaurantEntity.Id = Guid.NewGuid().ToString();
            //TODO: Check if geotag exist
            var algoAcc = new Account();
            restaurantEntity.AlgorandAddress = algoAcc.Address.ToString();

            _gretaFoodDb.Restaurants.Add(restaurantEntity);
            await _gretaFoodDb.SaveChangesAsync();
            
            return restaurantEntity;
        }
    }
}