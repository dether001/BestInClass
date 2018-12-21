using BestinClass.Core.Application_Service.Impl;
using BestinClass.Infrastructure.Data;
using System;
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BestinClass.Infrastructure.Data.Repositories;
using BestinClass.Core.Entity;
using System.IO;
using System.Collections.Generic;

namespace BestinClass.XUnitTest
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
            var created = carService.CreateCar(carService.NewCar("h", "g", 1999, "f", null, "hgffdvdfvfv"));
            Assert.Same(created, carService.GetCarById(created.Id));
        }

        [Fact]
        public void Test_GetCarByIdExceptions()
        {
            Assert.Throws<FileNotFoundException>(
                () => carService.GetCarById(965656666));
            Assert.Throws<FileNotFoundException>(
                () => carService.GetCarById(-9656));
        }

        [Fact]
        public void Test_GetAllCars()
        {
            var created = carService.CreateCar(carService.NewCar("h", "f", 1995, "kmlmklkl", null, "glmlkmkl"));
            Assert.Contains(created, carService.GetAllCars().List);
        }

        [Fact]
        public void Test_GetAllCarsExceptions()
        {
            foreach (var item in carService.GetAllCars().List)
            {
                carService.DeleteCar(item.Id);
            }
            Assert.Throws<FileNotFoundException>(
            () => carService.GetAllCars());
        }

        #endregion

        #region CreateCarTests
        [Fact]
        public void Test_CreateCar()
        {
            var created = carService.CreateCar(
                carService.NewCar("h", "f", 1995, "kmlmklkl", null, "glmlkmkl"));
            Assert.Same(created, carService.GetCarById(created.Id));
        }

        [Fact]
        public void Test_CreateCarExceptions()
        {
            //Year property exceptions
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", DateTime.Now.Year + 1, "g", null, "fvfdv")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", 1878, "g", null, "fvfdv")));

            //Model property exceptions
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "", 2000, "g", null, "fvfdv")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "bmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooook", 2000, "g", null, "fvfdv")));

            //Make property exceptions, minimum and maximum boundary
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("", "g", 2000, "g", null, "fvfdv")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("bmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooook", "g", 2000, "g", null, "fvfdv")));

            //Type property exceptions, minimum and maximum boundary
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", 2000, "", null, "fvfdv")));
            Assert.Throws<InvalidDataException>(
                () => carService.CreateCar(carService.NewCar("g", "g", 2000, "bmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooook", null, "fvfdv")));

        }

        #endregion

        #region DeleteCarTests
        [Fact]
        public void Test_DeleteCar()
        {
            var created = carService.CreateCar(
                carService.NewCar("kkkkkk", "kkkkkk", 2000, "kkkkkk", null, "fvfdv"));
            Assert.Contains(created, carService.GetAllCars().List);
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
