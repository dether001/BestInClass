using System.Collections.Generic;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        
        // GET api/review
        [HttpGet]
        public ActionResult<IEnumerable<Review>> Get()
        {
            return _reviewService.GetAllReviews();
        }

//        // GET api/review/carId=3
//        [HttpGet("carId={id}")]
//        public ActionResult<IEnumerable<Review>> Get(float carId)
//        {
//            int carToGet = (int) carId;
//            return _reviewService.GetReviewsByCar(carToGet);
//        }
        
        // GET api/review/2
        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            return _reviewService.GetReviewByIdIncludeCar(id);
        }
        
        // POST api/review
        [HttpPost]
        public ActionResult<Review> Post([FromBody] Review review)
        {
            return _reviewService.CreateReview(review);
        }
        
        // PUT api/review/3
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Review reviewUpdate)
        {
            _reviewService.UpdateReview(reviewUpdate);
        }
        
        // DELETE api/review/4
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _reviewService.DeleteReview(id);
        }
    }
}