using System.Collections.Generic;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ICarService _carService;

        public CarReviewController(IReviewService reviewService, ICarService carService)
        {
            _reviewService = reviewService;
            _carService = carService;
        }

        // GET api/review/3
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Review>> Get(int id)
        {
            return _reviewService.GetReviewsByCar(id);
        }
        // PUT api/review/3
        [HttpPost("{id}")]
        public void Put(int id, [FromBody] Review review)
        {
            _carService.AddReviewToCar(id, review);
        }
    }
}