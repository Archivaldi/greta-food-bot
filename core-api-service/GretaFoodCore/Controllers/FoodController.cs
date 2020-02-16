using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algorand;
using Algorand.Algod.Client.Api;
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
                    .Select(f => new
                    {
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
            var result = _gretaFoodDb.Foods
                .Select(f => new
                {
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
                throw new ApplicationException("Unable to parse input entity");
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
                throw new ApplicationException("Unable to find food to delete");
            }

            var restr = _gretaFoodDb.Restaurants.FirstOrDefault(r => r.Id.Equals(foodToDelete.RestaurantId));
            
            _gretaFoodDb.Foods.Remove(foodToDelete);
            await _gretaFoodDb.SaveChangesAsync();
            
            InCreaseKarmaForTheRestraunt(restr);
        }

        private void InCreaseKarmaForTheRestraunt(RestaurantEntity restaurantEntity)
        {
            if (restaurantEntity == null)
            {
                return;
            }
            try
            {
                Console.WriteLine("Increasing Karma on the BlockChain");
                string ALGOD_API_ADDR = "http://hackathon.algodev.network:9100";
                string ALGOD_API_TOKEN =
                    "ef920e2e7e002953f4b29a8af720efe8e4ecc75ff102b165e0472834b25832c1"; //find in the algod.token
                var algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);

                var transParams = algodApiInstance.TransactionParams();
                ulong? feePerByte = transParams.Fee;
                var genesisHash = new Digest(Convert.FromBase64String(transParams.Genesishashb64));
                var genesisID = transParams.GenesisID;
                var s = algodApiInstance.GetStatus();
                var firstRound = s.LastRound;
                Console.WriteLine("Current Round: " + firstRound);

                ulong? amount = 100000;
                ulong? lastRound = firstRound + 1000; // 1000 is the max tx window
                string SRC_ACCOUNT =
                    "typical permit hurdle hat song detail cattle merge oxygen crowd arctic cargo smooth fly rice vacuum lounge yard frown predict west wife latin absent cup";
                Account src = new Account(SRC_ACCOUNT);
                Transaction tx = new Transaction(src.Address, new Address(restaurantEntity.AlgorandAddress), amount, firstRound, lastRound, genesisID,
                    genesisHash);
                SignedTransaction signedTx = src.SignTransactionWithFeePerByte(tx, feePerByte.Value);

                //encode to msg-pack
                var encodedMsg = Algorand.Encoder.EncodeToMsgPack(signedTx);
                var id = algodApiInstance.RawTransaction(encodedMsg);
                Console.WriteLine("Successfully sent tx with id: " + id.TxId);
            }
            catch (Exception e)
            {
                // This is generally expected, but should give us an informative error message.
                Console.WriteLine("Exception when calling algod#rawTransaction: " + e.Message);
            }
            
             try
            {
                Console.WriteLine("Increasing Karma on The DB");
                restaurantEntity.KarmaScore++;
                _gretaFoodDb.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Was not manage to increase karma in the DB");
            }
        }
    }
}