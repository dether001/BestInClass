using System.Collections.Generic;
using System.IO;
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
        
        public Review NewReview(Car car, string header, string body, int ratingEveryday, int ratingWeekend, int ratingPracticality,
            int ratingExterior, int ratingInterior)
        {
            var review = new Review()
            {
                Header = header,
                Body = body,
                Car = car,
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
            if (review.Header.Length < 1)
                { throw new InvalidDataException("To create review, a header must be attached."); }
            if (review.Body.Length < 1)
                { throw new InvalidDataException("To create review, a body must be attached."); }
            if (review.RatingEveryday < 0 || review.RatingExterior < 0 || review.RatingInterior < 0 ||
                review.RatingOverall < 0 || review.RatingPracticality < 0 || review.RatingWeekend < 0)
                { throw new InvalidDataException("Review ratings can't be below 0."); }

            float allRatings = (review.RatingEveryday + review.RatingWeekend + review.RatingPracticality +
                                review.RatingExterior + review.RatingInterior);
            review.RatingOverall = allRatings / 5;
            return _reviewRepository.CreateReview(review);
        }

        public FilteredList<Review> GetAllReviews(PageFilter filter = null)
        {
            if (_reviewRepository.ReadAllReviews(filter).Count < 1)
            { throw new FileNotFoundException("Database is empty."); }
            return _reviewRepository.ReadAllReviews(filter);
        }

        public List<Review> GetReviewsByCar(int carId)
        {
            return _reviewRepository.ReadReviewsByCarId(carId).ToList();
        }

        public Review GetReviewByIdIncludeCar(int id)
        {
            return _reviewRepository.GetReviewByIdIncludeCar(id);
        }

        public Review GetReviewById(int id)
        {
            if (_reviewRepository.GetReviewById(id) == null)
            { throw new FileNotFoundException("No match were found."); }
            return _reviewRepository.GetReviewById(id);
        }

        public Review UpdateReview(Review reviewUpdate)
        {
            return _reviewRepository.UpdateReview(reviewUpdate);
        }

        public void DeleteReview(int id)
        {
            if (_reviewRepository.GetReviewById(id) == null)
            { throw new FileNotFoundException("No match were found."); }

            _reviewRepository.DeleteReview(id);
        }
    }
}