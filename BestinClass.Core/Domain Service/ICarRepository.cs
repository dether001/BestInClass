using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Domain_Service
{
    public interface ICarRepository
    {
        //CREATE
        Car CreateCar(Car car);

        //READ
        IEnumerable<Car> ReadAllCars();

        Car GetCarById(int id);

        //UPDATE
        Car UpdateCar(Car carUpdate);

        //DELETE
        Car DeleteCar(int id);
    }
}