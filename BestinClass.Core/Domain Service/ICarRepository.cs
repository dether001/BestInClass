using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Domain_Service
{
    public interface ICarRepository
    {
        //CREATE
        Car CreateCar(Car car);

        //READ
        FilteredList<Car> ReadAllCars(PageFilter filter);
        Car GetCarByIdIncludeReviews(int id);
        Car GetCarById(int id);

        //UPDATE
        Car UpdateCar(Car carUpdate);
        Car AddReviewToCar(int id, Review review);

        //DELETE
        Car DeleteCar(int id);
    }
}