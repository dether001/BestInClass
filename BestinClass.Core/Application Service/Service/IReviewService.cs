using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Service
{
    public interface IReviewService
    {
        //CREATE
        Review NewReview(Car car,string header, string body, int ratingEveryday, int ratingWeekend, int ratingPracticality, int ratingExterior, int ratingInterior);
        Review CreateReview(Review review);
        
        //READ
        FilteredList<Review> GetAllReviews(PageFilter filter);
        List<Review> GetReviewsByCar(int carId);
        Review GetReviewByIdIncludeCar(int id);
        Review GetReviewById(int id);

        //UPDATE
        Review UpdateReview(Review reviewUpdate);

        //DELETE
        void DeleteReview(int id);
    }
}