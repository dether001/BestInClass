using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Domain_Service
{
    public interface INewsRepository
    {
        //CREATE
        News CreateNews(News news);

        //READ
        IEnumerable<News> ReadAllNews();

        News GetNewsById(int id);

        //UPDATE
        News UpdateNews(News newsUpdate);

        //DELETE
        News DeleteNews(int id);
    }
}