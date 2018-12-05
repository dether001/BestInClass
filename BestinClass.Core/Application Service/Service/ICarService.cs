using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Service
{
    public interface ICarService
    {
        //CREATE
        Car NewCar(string make, string model, int year, string type);
        Car CreateCar(Car car);
        
        //READ
        List<Car> GetAllCars();
        Car GetCarById(int id);

        //UPDATE
        Car UpdateCar(Car carUpdate);

        //DELETE
        void DeleteCar(int id);
    }
}