using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            if (car.Year > System.DateTime.Now.Year || car.Year < 1879)
                { throw new InvalidDataException("To create a car, you need a proper year attached, between 1879 and the present date."); }
            if (car.Model.Length < 1 || car.Model.Length > 100)
            { throw new InvalidDataException("To create a car, you need a proper model attached, between 0 and 101 characters."); }
            if (car.Type.Length < 1 || car.Type.Length > 100)
            { throw new InvalidDataException("To create a car, you need a proper type attached, between 0 and 101 characters."); }
            if (car.Make.Length < 1 || car.Make.Length > 100)
            { throw new InvalidDataException("To create a car, you need a proper make attached, between 0 and 101 characters."); }

            return _carRepository.CreateCar(car);
        }
        
        public List<Car> GetAllCars()
        {
            if(_carRepository.ReadAllCars().Count() < 1)
                { throw new FileNotFoundException("Database is empty."); }
            return _carRepository.ReadAllCars().ToList();
        }

        public Car GetCarById(int id)
        {
            if(_carRepository.GetCarById(id) == null)
                { throw new FileNotFoundException("Database has no match."); }
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
            if (_carRepository.GetCarById(id) == null)
            { throw new FileNotFoundException("Database has no match."); }
            _carRepository.DeleteCar(id);
        }
    }
}