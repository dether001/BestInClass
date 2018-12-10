using System.Collections.Generic;

namespace BestinClass.Core.Entity
{
    public class Review
    {
        public int Id { get; set; }
        //public List<CarReview> CarReviews { get; set; }
        public Car Car { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public int RatingEveryday { get; set; }
        public int RatingWeekend { get; set; }
        public int RatingPracticality { get; set; }
        public int RatingExterior { get; set; }
        public int RatingInterior { get; set; }
        public float RatingOverall { get; set; }
    }
}