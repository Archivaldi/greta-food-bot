using System;

namespace GretaFoodBot
{
    public class ClosestFoodDto
    {
        public Food Food { get; set; }
        public long DistanceInMeters { get; set; }
        public DateTime ArivalTime { get; set; }
    }
}