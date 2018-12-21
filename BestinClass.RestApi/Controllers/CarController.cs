using System;
using System.Collections.Generic;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        
        //GET api/car
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get([FromQuery] PageFilter filter)
        {
            try
            {
                if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
                {
                    var list = _carService.GetAllCars(null);
                    return Ok(list);
                }
                else
                {
                    var list = _carService.GetAllCars(filter);
                    return Ok(list);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        // GET api/car/2
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            return _carService.GetCarByIdIncludeReviews(id);
        }
        
        // POST api/car
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            return _carService.CreateCar(car);
        }
        
        // PUT api/car/3
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Car carUpdate)
        {
            _carService.UpdateCar(carUpdate);
        }
        
        // DELETE api/car/4
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _carService.DeleteCar(id);
        }
    }
}