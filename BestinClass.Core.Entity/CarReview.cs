namespace BestinClass.Core.Entity
{
    public class CarReview
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}