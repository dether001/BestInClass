using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Impl
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        public Car NewCar(string make, string model, int year, string type, List<Review> reviews, string picture)
        {
            var car = new Car()
            {
                Make = make,
                Model = model,
                Year = year,
                Type = type,
                Reviews = reviews,
                Picture = picture
            };

            return car;
        }

        public Car CreateCar(Car car)
        {
            return _carRepository.CreateCar(car);
        }

        public List<Car> GetAllCars()
        {
            return _carRepository.ReadAllCars().ToList();
        }

        public Car GetCarById(int id)
        {
            return _carRepository.GetCarById(id);
        }

        public Car GetCarByIdIncludeReviews(int id)
        {
            return _carRepository.GetCarByIdIncludeReviews(id);
        }

        public Car UpdateCar(Car carUpdate)
        {
            return _carRepository.UpdateCar(carUpdate);
        }

        public Car AddReviewToCar(int id, Review review)
        {
            return _carRepository.AddReviewToCar(id, review);
        }

        public void DeleteCar(int id)
        {
            _carRepository.DeleteCar(id);
        }
    }
}