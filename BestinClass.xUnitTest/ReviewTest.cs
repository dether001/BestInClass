using System;
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BestinClass.Infrastructure.Data;
using BestinClass.Infrastructure.Data.Repositories;
using BestinClass.Core.Application_Service.Impl;
using System.IO;
using BestinClass.Core.Entity;
using System.Linq;
using System.Collections.Generic;

namespace BestinClass.XUnitTest
{
    public class ReviewTest : IDisposable
    {
        readonly SqliteConnection connection;
        readonly ReviewService reviewService;
        readonly CarService carService;

        public ReviewTest()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<BestinClassContext>()
                            .UseSqlite(connection).Options;
            var dbContext = new BestinClassContext(options);
            DBInitializer.SeedDB(dbContext);

            var reviewRepo = new ReviewRepository(dbContext);
            reviewService = new ReviewService(reviewRepo);

            var carRepo = new CarRepository(dbContext);
            carService = new CarService(carRepo);
        }

        public void Dispose()
        {
            connection.Close();
        }
        
        #region CreateReviewTests
        [Fact]
        public void Test_CreateReview()
        {
            //I can't seem to find a way to properly create a review for testing.

            var re = new Review
            {
                Body = "f",
                Car = null,
                Header = "",
                RatingEveryday = 1,
                RatingExterior = 2,
                RatingInterior = 1,
                RatingOverall = 1,
                RatingPracticality = 1,
                RatingWeekend = 1
            };

            reviewService.CreateReview(re);

            var car = carService.CreateCar(
                carService.NewCar("dfkkmghj", "fdghdf", 1995, "sdfghjk", null, "dfghj"));
            var review = reviewService.CreateReview(
                reviewService.NewReview(car, "g", "g", 5, 5, 5, 5, 5));
            Assert.Same(review, reviewService.GetReviewById(review.Id));
        }

        [Fact]
        public void Test_CreateReviewExceptions()
        {
            Car car = reviewService.GetAllReviews().First().Car;
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(car, "", "g", 1, 1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(car, "g", "", 1, 1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(car, "g", "g", -1, 1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 1, -1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 1, 1, -1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 1, 1, 1, -1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 1, 1, 1, 1, -1)));
        }
        #endregion

        #region GetReviewsTests
        [Fact]
        public void Test_GetReview()
        {
            Car car = reviewService.GetAllReviews().First().Car;
            var created = reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 5, 5, 5, 5, 5));
            Assert.Same(created, reviewService.GetReviewById(created.Id));
        }

        [Fact]
        public void Test_GetReviewExceptions()
        {
            Assert.Throws<FileNotFoundException>(
                () => reviewService.GetReviewById(-5));
            Assert.Throws<FileNotFoundException>(
                () => reviewService.GetReviewById(999999999));
        }

        [Fact]
        public void Test_GetAllReviews()
        {
            Car car = reviewService.GetAllReviews().First().Car;
            var created = reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 5, 5, 5, 5, 5));
            Assert.Contains(created, reviewService.GetAllReviews());
            var created2 = reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 5, 5, 5, 5, 5));
            Assert.Contains(created2, reviewService.GetAllReviews());
            var created3 = reviewService.CreateReview(reviewService.NewReview(car, "g", "g", 5, 5, 5, 5, 5));
            Assert.Contains(created3, reviewService.GetAllReviews());
        }

        [Fact]
        public void Test_GetAllReviewsExceptions()
        {
            Assert.Throws<FileNotFoundException>(
                () => reviewService.GetAllReviews());
        }
        #endregion

        #region DeleteReviewsTests
        [Fact]
        public void Test_DeleteReview()
        {
            Car car = reviewService.GetAllReviews().First().Car;
            var created = reviewService.CreateReview
                (reviewService.NewReview(car, "g", "g", 5, 5, 5, 5, 5));
            Assert.Contains(created, reviewService.GetAllReviews());
            reviewService.DeleteReview(created.Id);
            Assert.Throws<FileNotFoundException>(
                () => reviewService.GetReviewById(created.Id));
        }
        #endregion

        #region UpdateReviewTests
        [Fact]
        public void Test_UpdateReview()
        {

        }
        #endregion
    }
}
