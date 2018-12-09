using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Impl
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        
        public Review NewReview(int carId, string header, string body, int ratingEveryday, int ratingWeekend, int ratingPracticality,
            int ratingExterior, int ratingInterior)
        {
            var review = new Review()
            {
                CarId = carId,
                Header = header,
                Body = body,
                RatingEveryday = ratingEveryday,
                RatingWeekend = ratingWeekend,
                RatingPracticality = ratingPracticality,
                RatingExterior = ratingExterior,
                RatingInterior = ratingInterior,
                RatingOverall = (ratingEveryday + ratingWeekend + ratingPracticality + ratingExterior + ratingInterior) / 5
            };

            return review;
        }

        public Review CreateReview(Review review)
        {
            return _reviewRepository.CreateReview(review);
        }

        public List<Review> GetAllReviews()
        {
            return _reviewRepository.ReadAllReviews().ToList();
        }

        public Review GetReviewById(int id)
        {
            return _reviewRepository.GetReviewById(id);
        }

        public Review UpdateReview(Review reviewUpdate)
        {
            return _reviewRepository.UpdateReview(reviewUpdate);
        }

        public void DeleteReview(int id)
        {
            _reviewRepository.DeleteReview(id);
        }
    }
}