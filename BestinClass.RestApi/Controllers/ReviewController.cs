using System;
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
        public ActionResult<IEnumerable<Review>> Get([FromQuery] PageFilter filter)
        {
            try
            {
                if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
                {
                    var list = _reviewService.GetAllReviews(null);
                    return Ok(list);
                }
                else
                {
                    var list = _reviewService.GetAllReviews(filter);
                    return Ok(list);
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            return _reviewService.GetReviewById(id);
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