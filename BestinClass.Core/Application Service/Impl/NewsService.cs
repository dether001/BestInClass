using System;
using System.Collections.Generic;
using System.IO;
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
            if (news.Picture.Length < 1)
                { throw new InvalidDataException("To create news, a picture must be attached."); }
            if (news.Tags.Length < 1)
                { throw new InvalidDataException("To create news, tags must be attached."); }
            if (news.Header.Length < 1)
                { throw new InvalidDataException("To create news, a header must be attached."); }
            if (news.Body.Length < 1)
                { throw new InvalidDataException("To create news, a body must be attached."); }
            if (news.ShortDesc.Length < 1 || news.ShortDesc.Length > 255)
                { throw new InvalidDataException("To create news, your description must be between 0 and 256 characters."); }
            
            return _newsRepository.CreateNews(news);
        }

        public FilteredList<News> GetAllNews(PageFilter filter = null)
        {
            if (_newsRepository.ReadAllNews(filter).Count < 1)
                { throw new FileNotFoundException("Database is empty."); }
            return _newsRepository.ReadAllNews(filter);
        }

        public News GetNewsById(int id)
        {
            if (_newsRepository.GetNewsById(id) == null)
                { throw new FileNotFoundException("Database found no match."); }

            return _newsRepository.GetNewsById(id);
        }

        public News UpdateNews(News newsUpdate)
        {
            return _newsRepository.UpdateNews(newsUpdate);
        }

        public void DeleteNews(int id)
        {
            if (_newsRepository.GetNewsById(id) == null)
            { throw new FileNotFoundException("Database has no match."); }

            _newsRepository.DeleteNews(id);
        }
    }
}