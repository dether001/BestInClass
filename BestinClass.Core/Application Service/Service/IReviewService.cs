using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Service
{
    public interface IReviewService
    {
        //CREATE
        Review NewReview(int carId, string header, string body, int ratingEveryday, int ratingWeekend, int ratingPracticality, int ratingExterior, int ratingInterior);
        Review CreateReview(Review review);
        
        //READ
        List<Review> GetAllReviews();
        Review GetReviewById(int id);

        //UPDATE
        Review UpdateReview(Review reviewUpdate);

        //DELETE
        void DeleteReview(int id);
    }
}