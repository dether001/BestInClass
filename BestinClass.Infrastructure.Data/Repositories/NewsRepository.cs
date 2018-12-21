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

        public FilteredList<News> ReadAllNews(PageFilter filter)
        {
            var filteredList = new FilteredList<News>();

            if (filter != null && filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                filteredList.List = _ctx.News
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
                filteredList.Count = _ctx.News.Count();
                return filteredList;
            }

            filteredList.List = _ctx.News;
            filteredList.Count = _ctx.News.Count();
            return filteredList;
        }

        public News GetNewsById(int id)
        {
            return _ctx.News
                .FirstOrDefault(n => n.Id == id);
        }

        public News UpdateNews(News newsUpdate)
        {
            _ctx.Attach(newsUpdate).State = EntityState.Modified;
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