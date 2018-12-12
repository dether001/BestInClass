using BestinClass.Core.Application_Service.Impl;
using BestinClass.Infrastructure.Data;
using BestinClass.Infrastructure.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BestinClass.xUnitTest
{
    public class NewsTest : IDisposable
    {
        readonly SqliteConnection connection;
        readonly NewsService newsService;

        public NewsTest()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Initialize test database
            var options = new DbContextOptionsBuilder<BestinClassContext>()
                            .UseSqlite(connection).Options;
            var dbContext = new BestinClassContext(options);
            DBInitializer.SeedDB(dbContext);

            var newsRepo = new NewsRepository(dbContext);
            newsService = new NewsService(newsRepo);
        }

        public void Dispose()
        {
            connection.Close();
        }

        #region CreateNewsTests
        [Fact]
        public void Test_CreateNews()
        {
            var created = newsService.CreateNews(newsService.NewNews("k", "h", "f", "t", "d"));
            Assert.Same(created, newsService.GetNewsById(created.Id));

        }

        [Fact]
        public void Test_CreateNewsExceptions()
        {
            //Checks News properties for having any content, individually.
            Assert.Throws<InvalidDataException>(
                () => newsService.CreateNews(newsService.NewNews("", "g", "f", "t", "d")));
            Assert.Throws<InvalidDataException>(
                () => newsService.CreateNews(newsService.NewNews("k", "", "f", "t", "d")));
            Assert.Throws<InvalidDataException>(
                () => newsService.CreateNews(newsService.NewNews("k", "g", "", "t", "d")));
            Assert.Throws<InvalidDataException>(
                () => newsService.CreateNews(newsService.NewNews("k", "g", "f", "", "d")));
            Assert.Throws<InvalidDataException>(
                () => newsService.CreateNews(newsService.NewNews("k", "g", "f", "t", "")));
            //Tests the maximum boundry of the shortDescription.
            Assert.Throws<InvalidDataException>(
                () => newsService.CreateNews(newsService.NewNews("k", "g", "bmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooookbmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkoooooobmkgbmgklbklgbmkglmbkmbklmbklgmfklbmgfkblgfkooooookhgklfmgklfkglfmklgfmlklmkkmggpmpewmsdldfvbhbhebhjbhbab", "t", "d")));

        }
        #endregion

        #region GetNewsTests
        [Fact]
        public void Test_GetNewsById()
        {
            var created = newsService.CreateNews(newsService.NewNews("k", "g", "f", "t", "d"));
            Assert.Same(created, newsService.GetNewsById(created.Id));
        }

        [Fact]
        public void Test_GetNewsByIdException()
        {
            Assert.Throws<FileNotFoundException>(
                () => newsService.GetNewsById(-5555));
            Assert.Throws<FileNotFoundException>(
                () => newsService.GetNewsById(9995555));
        }

        [Fact]
        public void Test_GetAllNews()
        {
            var created = newsService.CreateNews(newsService.NewNews("k", "g", "f", "t", "d"));
            Assert.Contains(created, newsService.GetAllNews());
            var created2 = newsService.CreateNews(newsService.NewNews("k", "g", "f", "t", "d"));
            Assert.Contains(created2, newsService.GetAllNews());
            var created3 = newsService.CreateNews(newsService.NewNews("k", "g", "f", "t", "d"));
            Assert.Contains(created3, newsService.GetAllNews());

        }

        [Fact]
        public void Test_GetAllNewsExceptions()
        {
            Assert.Throws<FileNotFoundException>(
                () => newsService.GetAllNews());
        }
        #endregion

        #region DeleteNewsTests
        [Fact]
        public void Test_DeleteNews()
        {
            var created = newsService.CreateNews
                (newsService.NewNews("lp", "fn", "jh", "yg", "a"));
            Assert.Contains(created, newsService.GetAllNews());
            newsService.DeleteNews(created.Id);
            Assert.Throws<FileNotFoundException>(
                () => newsService.GetNewsById(created.Id));
        }
        #endregion

        #region UpdateNewsTests
        [Fact]
        public void Test_UpdateNews()
        {

        }
        #endregion
    }
}
