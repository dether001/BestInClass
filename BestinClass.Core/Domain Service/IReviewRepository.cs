using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Domain_Service
{
    public interface IReviewRepository
    {
        //CREATE
        Review CreateReview(Review review);

        //READ
        IEnumerable<Review> ReadAllReviews();
        Review GetReviewByIdIncludeCar(int id);
        IEnumerable<Review> ReadReviewsByCarId(int carId);
        Review GetReviewById(int id);

        //UPDATE
        Review UpdateReview(Review reviewUpdate);

        //DELETE
        Review DeleteReview(int id);
    }
}