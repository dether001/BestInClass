using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BestinClass.Infrastructure.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BestinClassContext _ctx;

        public ReviewRepository(BestinClassContext ctx)
        {
            _ctx = ctx;
        }
        
        public Review CreateReview(Review review)
        {
            _ctx.Attach(review).State = EntityState.Added;
            _ctx.SaveChanges();
            return review;
        }

        public IEnumerable<Review> ReadAllReviews()
        {
            return _ctx.Review;
        }

        public Review GetReviewById(int id)
        {
            return _ctx.Review
                .FirstOrDefault(r => r.Id == id);
        }

        public Review UpdateReview(Review reviewUpdate)
        {
            _ctx.Attach(reviewUpdate).State = EntityState.Added;
            _ctx.SaveChanges();
            return reviewUpdate;
        }

        public Review DeleteReview(int id)
        {
            var removed = _ctx.Remove(new Review {Id = id}).Entity;
            _ctx.SaveChanges();
            return removed;
        }
    }
}