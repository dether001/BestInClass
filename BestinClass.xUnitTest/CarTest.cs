using BestinClass.Core.Application_Service.Impl;
using BestinClass.Infrastructure.Data;
using System;
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BestinClass.Infrastructure.Data.Repositories;
using BestinClass.Core.Entity;
using System.IO;

namespace BestinClass.xUnitTest
{
    public class CarTest
    {
        readonly SqliteConnection connection;
        readonly CarService carService;

        public CarTest()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<BestinClassContext>()
                            .UseSqlite(connection).Options;
            var dbContext = new BestinClassContext(options);
            DBInitializer.SeedDB(dbContext);
            
            var carRepo = new CarRepository(dbContext);
            carService = new CarService(carRepo);
        }

        #region GetCarsTests
        [Fact]
        public void Test_GetCarById()
        {
            var created = carService.CreateCar(carService.NewCar("h", "g", 1999, "f"));
            Assert.Same(created, carService.GetCarById(created.Id));
        }

        [Fact]
        public void TestGetCarByIdExceptions()
        {
            Assert.Throws<FileNotFoundException>(
                () => carService.GetCarById(965656666));
            Assert.Throws<FileNotFoundException>(
                () => carService.GetCarById(-9656));
        }

        [Fact]
        public void Test_GetAllCars()
        {
            var created = carService.CreateCar(carService.NewCar("h", "f", 1995, "g"));
            Assert.Contains(created, carService.GetAllCars());
        }

        [Fact]
        public void Test_GetAllCarsExceptions()
        {
            Assert.Throws<FileNotFoundException>(
            () => carService.GetAllCars());
        }

        #endregion

        #region CreateCarTests
        [Fact]
        public void Test_CreateCar()
        {
            var created = carService.CreateCar(
                carService.NewCar("hhhhh", "ggggg", 2000, "ttttttt"));
            Assert.Same(created, carService.GetCarById(created.Id));
        }

        [Fact]
        public void Test_CreateCarExceptions()
        {
            //Year property exceptions
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", DateTime.Now.Year+1, "g")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", 1878, "g")));

            //Model property exceptions
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "", 2000, "g")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "bmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooook", 2000, "g")));

            //Make property exceptions
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("", "g", 2000, "g")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("bmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooook", "g", 2000, "g")));
            
            //Type property exceptions
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", 2000, "")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", 2000, "bmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooook")));

        }

        #endregion

        #region DeleteCarTests
        [Fact]
        public void Test_DeleteCar()
        {
            var created = carService.CreateCar(
                carService.NewCar("kkkkkk", "kkkkkk", 2000, "kkkkkk"));
            Assert.Contains(created, carService.GetAllCars());
            carService.DeleteCar(created.Id);
            Assert.Throws<FileNotFoundException>(
                () => carService.GetCarById(created.Id));

        }

        #endregion

        #region UpdateCarTests
        [Fact]
        public void Test_UpdateCar()
        {
            
        }

        #endregion
    }
}
