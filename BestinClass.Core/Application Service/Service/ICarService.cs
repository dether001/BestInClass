using System.Collections;
using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Service
{
    public interface ICarService
    {
        //CREATE
        Car NewCar(string make, string model, int year, string type, List<Review> reviews, string picture);
        Car CreateCar(Car car);
        
        //READ
        List<Car> GetAllCars();
        Car GetCarById(int id);
        Car GetCarByIdIncludeReviews(int id);

        //UPDATE
        Car UpdateCar(Car carUpdate);
        Car AddReviewToCar(int id, Review review);

        //DELETE
        void DeleteCar(int id);
    }
}