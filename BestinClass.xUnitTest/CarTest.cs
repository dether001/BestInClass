using BestinClass.Core.Application_Service.Impl;
using BestinClass.Infrastructure.Data;
using System;
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BestinClass.Infrastructure.Data.Repositories;
using System.Linq;

namespace BestinClass.xUnitTest
{
    public class CarTest : IDisposable
    {
        SqliteConnection connection;
        CarService carService;

        public CarTest()
        {
            connection = new SqliteConnection("DataSource=:memory:");

            // In-memory database only exists while the connection is open
            connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<BestinClassContext>()
                            .UseSqlite(connection).Options;
            var dbContext = new BestinClassContext(options);
            DBInitializer.SeedDB(dbContext);

            // Create repositories and services
            var carRepo = new CarRepository(dbContext);
            carService = new CarService(carRepo);
        }

        public void Dispose()
        {
            // This will delete the in-memory database
            connection.Close();
        }

        #region GetCarsTests
        [Fact]
        public void Test_GetCar()
        {
            Assert.Null(carService.GetCarById(1));
            var car = carService.NewCar("h", "g", 9, "f");
            carService.CreateCar(car);
            Assert.NotNull(carService.GetCarById(1));
        }

        [Fact]
        public void Test_GetAllCars()
        {
            var allCars = carService.GetAllCars();
            Assert.Empty(allCars);
        }

        #endregion

        #region CreateCarTests
        [Fact]
        public void Test_CreateCar()
        {
            Assert.Empty(carService.GetAllCars());
            var car = carService.NewCar("h", "g", 9, "f");
            carService.CreateCar(car);
            Assert.NotEmpty(carService.GetAllCars());
            Assert.Same(car, carService.GetCarById(1));
            Assert.Single(carService.GetAllCars());
        }

        #endregion

        #region DeleteCarTests
        [Fact]
        public void Test_DeleteCar()
        {
            Assert.Empty(carService.GetAllCars());
            var car1 = carService.NewCar("h", "g", 9, "f");
            carService.CreateCar(car1);
            Assert.Single(carService.GetAllCars());
            //idk
            carService.DeleteCar(1);
            Assert.Empty(carService.GetAllCars());

            
            
        }

        #endregion
    }
}
