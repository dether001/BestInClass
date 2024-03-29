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
            _ctx.Attach(review.Car);
            var saved = _ctx.Review.Add(review).Entity;
            _ctx.SaveChanges();
            return saved;
        }

        public FilteredList<Review> ReadAllReviews(PageFilter filter)
        {
            var filteredList = new FilteredList<Review>();

            if(filter != null && filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                filteredList.List = _ctx.Review
                    .Include(r => r.Car)
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
                filteredList.Count = _ctx.Review.Count();
                return filteredList;
            }

            filteredList.List = _ctx.Review.Include(r => r.Car);
            filteredList.Count = _ctx.Review.Count();
            return filteredList;
        }

        public Review GetReviewById(int id)
        {
            return _ctx.Review
                .FirstOrDefault(r => r.Id == id);
        }

        public Review GetReviewByIdIncludeCar(int id)
        {
            var currRev = _ctx.Review
                .Include(r => r.Car)
                .FirstOrDefault(c => c.Id == id);
            return currRev;
        }

        public Review UpdateReview(Review reviewUpdate)
        {
            _ctx.Attach(reviewUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return reviewUpdate;
        }

        public Review DeleteReview(int id)
        {
            var removed = _ctx.Review.FirstOrDefault(c => c.Id == id);
            _ctx.Remove(removed);
            _ctx.SaveChanges();
            return removed;
        }
    }
}