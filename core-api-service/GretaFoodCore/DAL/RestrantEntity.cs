using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GretaFoodCore.Api.DAL
{
    public class RestaurantEntity
    {
        [Key] [Column("Id")]
        public string Id { get; set; }

        [Column("Name")] [Required] 
        public string Name { get; set; }

        [Column("Address", TypeName = "VARCHAR(300)")]
        public string Address { get; set; }
        
        [Column("GeoTag")] [Required]
        public string GeoTag { get; set; }
        
        [Column("AlgorandAddress")]
        public string AlgorandAddress { get; set; }

        [Column("KarmaScore")]
        public int KarmaScore { get; set; }

        public List<FoodEntity> FoodEntities { get; set; }
    }
}