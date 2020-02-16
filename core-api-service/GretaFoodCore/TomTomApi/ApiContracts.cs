using System.Collections.Generic;
using Newtonsoft.Json;

namespace GretaFoodCore.Api.TomTomApi
{
    public partial class TomTomResponse
    {
        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("score")]
        public decimal Score { get; set; }

        [JsonProperty("dist")]
        public decimal Dist { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("poi")]
        public Poi Poi { get; set; }

        [JsonProperty("position")]
        public GeoBias Position { get; set; }

    }
    
    public partial class Poi
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }
    
    public partial class Summary
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("queryType")]
        public string QueryType { get; set; }

        [JsonProperty("queryTime")]
        public long QueryTime { get; set; }

        [JsonProperty("numResults")]
        public long NumResults { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("fuzzyLevel")]
        public long FuzzyLevel { get; set; }

        [JsonProperty("geoBias")]
        public GeoBias GeoBias { get; set; }
    }
    
    public partial class GeoBias
    {
        [JsonProperty("lat")]
        public decimal Lat { get; set; }

        [JsonProperty("lon")]
        public decimal Lon { get; set; }
    }
}