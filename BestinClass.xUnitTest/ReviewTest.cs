using System;
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BestinClass.Infrastructure.Data;
using BestinClass.Infrastructure.Data.Repositories;
using BestinClass.Core.Application_Service.Impl;
using System.IO;

namespace BestinClass.xUnitTest
{
    public class ReviewTest : IDisposable
    {
        readonly SqliteConnection connection;
        readonly ReviewService reviewService;

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
        }

        public void Dispose()
        {
            connection.Close();
        }

        #region CreateReviewTests
        [Fact]
        public void Test_CreateReview()
        {
            //This test doesn't varify the carId as it is just an int atm.
            var created = reviewService.CreateReview(reviewService.NewReview(1, "g", "g", 5, 5, 5, 5, 5));
            Assert.Same(created, reviewService.GetReviewById(created.Id));
        }

        [Fact]
        public void Test_CreateReviewExceptions()
        {
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(5, "", "g", 1, 1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(5, "g", "", 1, 1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(5, "g", "g", -1, 1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(5, "g", "g", 1, -1, 1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(5, "g", "g", 1, 1, -1, 1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(5, "g", "g", 1, 1, 1, -1, 1)));
            Assert.Throws<InvalidDataException>(
                () => reviewService.CreateReview(reviewService.NewReview(5, "g", "g", 1, 1, 1, 1, -1)));
        }
        #endregion

        #region GetReviewsTests
        [Fact]
        public void Test_GetReview()
        {
            var created = reviewService.CreateReview(reviewService.NewReview(1, "g", "g", 5, 5, 5, 5, 5));
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
            var created = reviewService.CreateReview(reviewService.NewReview(1, "g", "g", 5, 5, 5, 5, 5));
            Assert.Contains(created, reviewService.GetAllReviews());
            var created2 = reviewService.CreateReview(reviewService.NewReview(1, "g", "g", 5, 5, 5, 5, 5));
            Assert.Contains(created2, reviewService.GetAllReviews());
            var created3 = reviewService.CreateReview(reviewService.NewReview(1, "g", "g", 5, 5, 5, 5, 5));
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
            var created = reviewService.CreateReview
                (reviewService.NewReview(1, "g", "g", 5, 5, 5, 5, 5));
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
