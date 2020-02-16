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
        public List<FoodEntity> GetAllFood()
        {
            return  _gretaFoodDb.Foods.Include(f => f.Restaurant).ToList();
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