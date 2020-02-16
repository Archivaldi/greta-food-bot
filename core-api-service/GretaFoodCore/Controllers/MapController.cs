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
        private string _tomTomApiKey;
        public MapController(CommandLineArguments options)
        {
            _tomTomApiKey = options.TomTomApiKey;
        }
        
        [HttpGet("{searchString}")]
        public async Task<TomTomResponse> SearchInSF(string searchString)
        {
            var client = new RestClient("https://api.tomtom.com");
            var request = new RestRequest($"search/2/search/{searchString}.json");
            request.AddQueryParameter("countrySet", "US");
            //San Fran
            request.AddQueryParameter("lat", "37.783676");
            request.AddQueryParameter("lon", "-122.412736");

            request.AddQueryParameter("radius", "50000");
            request.AddQueryParameter("key", _tomTomApiKey);

            var response = await client.ExecuteTaskAsync<TomTomResponse>(request);
        
            return response.Data;
        }
    }
}