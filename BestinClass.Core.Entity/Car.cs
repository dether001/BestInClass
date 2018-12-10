using System.Collections;
using System.Collections.Generic;

namespace BestinClass.Core.Entity
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public List<CarReview> CarReviews { get; set; }
    }
}