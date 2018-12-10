using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BestinClass.Infrastructure.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly BestinClassContext _ctx;

        public CarRepository(BestinClassContext ctx)
        {
            _ctx = ctx;
        }
        
        public Car CreateCar(Car car)
        {
            _ctx.Attach(car).State = EntityState.Added;
            _ctx.SaveChanges();
            return car;
        }

        public IEnumerable<Car> ReadAllCars()
        {
            return _ctx.Car;
        }

        public Car GetCarByIdIncludeReviews(int id)
        {
            var currCar =_ctx.Car
                .Include(c => c.Reviews)
                .FirstOrDefault(c => c.Id == id);
            return currCar;
        }

        public Car AddReviewToCar(int id, Review review)
        {
            return null;
        }

        public Car GetCarById(int id)
        {
            return _ctx.Car
                .FirstOrDefault(c => c.Id == id);
        }

        public Car UpdateCar(Car carUpdate)
        {
            _ctx.Attach(carUpdate).State = EntityState.Added;
            _ctx.SaveChanges();
            return carUpdate;
        }

        public Car DeleteCar(int id)
        {
            var removed = _ctx.Remove(new Car {Id = id}).Entity;
            _ctx.SaveChanges();
            return removed;
        }
    }
}