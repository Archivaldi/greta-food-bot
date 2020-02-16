using System.Threading.Tasks;
using GretaFoodCore.Api.TomTomApi;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace GretaFoodCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController
    {
        
        // [HttpGet("{searchString}")]
        // public async Task<TomTomResponse> SearchInSF(string searchString)
        // {
        //     var client = new RestClient("https://api.tomtom.com");
        //     var response = await client.ExecuteTaskAsync<TomTomResponse>(request);
        //
        //     return response.Data;
        //     //return  _gretaFoodDb.Restaurants.ToList();
        // }
        // public void GetAllRestaurants()
        // {
        //     return  _gretaFoodDb.Restaurants.ToList();
        // }
    }
}