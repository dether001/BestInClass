using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BestinClass.Infrastructure.Data.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly BestinClassContext _ctx;

        public NewsRepository(BestinClassContext ctx)
        {
            _ctx = ctx;
        }
        
        public News CreateNews(News news)
        {
            _ctx.Attach(news).State = EntityState.Added;
            _ctx.SaveChanges();
            return news;
        }

        public IEnumerable<News> ReadAllNews()
        {
            return _ctx.News;
        }

        public News GetNewsById(int id)
        {
            return _ctx.News
                .FirstOrDefault(n => n.Id == id);
        }

        public News UpdateNews(News newsUpdate)
        {
            _ctx.Attach(newsUpdate).State = EntityState.Added;
            _ctx.SaveChanges();
            return newsUpdate;
        }

        public News DeleteNews(int id)
        {
            var removed = _ctx.News.FirstOrDefault(c => c.Id == id);
            _ctx.Remove(removed);
            _ctx.SaveChanges();
            return removed;
        }
    }
}