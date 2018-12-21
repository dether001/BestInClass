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

        public FilteredList<Car> ReadAllCars(PageFilter filter)
        {
            var filteredList = new FilteredList<Car>();

            if (filter != null && filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                filteredList.List = _ctx.Car
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
                filteredList.Count = _ctx.Car.Count();
                return filteredList;
            }

            filteredList.List = _ctx.Car;
            filteredList.Count = _ctx.Car.Count();
            return filteredList;
        }

        public Car GetCarByIdIncludeReviews(int id)
        {
            var currCar =_ctx.Car
                .Include(c => c.Reviews)
                .FirstOrDefault(c => c.Id == id);
            return currCar;
        }

        public Car GetCarById(int id)
        {
            return _ctx.Car
                .FirstOrDefault(c => c.Id == id);
        }

        public Car UpdateCar(Car carUpdate)
        {
            _ctx.Attach(carUpdate).State = EntityState.Modified;
            //_ctx.Update<Car>(carUpdate);
            _ctx.SaveChanges();
            return carUpdate;
        }

        public Car DeleteCar(int id)
        {
            var removed = _ctx.Car.FirstOrDefault(c => c.Id == id);
            _ctx.Remove(removed);
            _ctx.SaveChanges();
            return removed;
        }
    }
}