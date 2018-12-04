using System.Collections.Generic;
using BestInClass.Core.Entity;

namespace BestInClass.Core.Domain_Service
{
    public interface IReviewRepository
    {
        
        //CREATE
        Review CreateReview(Review review);
  
        //READ
        IEnumerable<Review> ReadAllReview();
        Review ReadReviewByIdIncludeProduct(int id);
        Review GetReviewById(int id);
        
        //UPDATE
        Review UpdateReview(Review updateReview);
        
        //DELETE
        Review DeleteReview(int id);
        
    }
}