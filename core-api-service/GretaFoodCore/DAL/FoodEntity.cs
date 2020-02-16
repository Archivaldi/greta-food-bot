using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GretaFoodCore.Api.DAL
{
    public class FoodEntity
    {
        [Key] [Column("Id")]
        public string Id { get; set; }

        [Column("Name")] [Required] 
        public string Name { get; set; }

        [ForeignKey("Restaurant")]
        public string RestaurantId { get; set; }
        public RestaurantEntity Restaurant { get; set; }
    }
}