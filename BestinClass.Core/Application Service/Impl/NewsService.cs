using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Impl
{
    public class NewsService : INewsService
    {

        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        
        public News NewNews(string header, string picture, string shortDesc, string body, string tags)
        {
            var news = new News()
            {
                Header = header,
                Picture = picture,
                ShortDesc = shortDesc,
                Body = body,
                Tags = tags
            };

            return news;
        }

        public News CreateNews(News news)
        {
            return _newsRepository.CreateNews(news);
        }

        public List<News> GetAllNews()
        {
            return _newsRepository.ReadAllNews().ToList();
        }

        public News GetNewsById(int id)
        {
            return _newsRepository.GetNewsByid(id);
        }

        public News UpdateNews(News newsUpdate)
        {
            return _newsRepository.UpdateNews(newsUpdate);
        }

        public void DeleteNews(int id)
        {
            _newsRepository.DeleteNews(id);
        }
    }
}