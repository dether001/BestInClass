using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Service
{
    public interface INewsService
    {
        //CREATE
        News NewNews(string header, string picture, string shortDesc, string body, string tags);
        News CreateNews(News news);
        
        //READ
        FilteredList<News> GetAllNews(PageFilter filter);
        News GetNewsById(int id);

        //UPDATE
        News UpdateNews(News newsUpdate);

        //DELETE
        void DeleteNews(int id);
    }
}