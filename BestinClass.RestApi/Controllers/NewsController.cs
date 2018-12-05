using System.Collections;
using System.Collections.Generic;
using BestinClass.Core.Application_Service.Impl;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        // GET api/news
        [HttpGet]
        public ActionResult<IEnumerable<News>> Get()
        {
            return _newsService.GetAllNews();
        }

        // GET api/news/2
        [HttpGet("{id}")]
        public ActionResult<News> Get(int id)
        {
            return _newsService.GetNewsById(id);
        }
        
        // POST api/news
        [HttpPost]
        public ActionResult<News> Post([FromBody] News news)
        {
            return _newsService.CreateNews(news);
        }
        
        // PUT api/news/3
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] News newsUpdate)
        {
            _newsService.UpdateNews(newsUpdate);
        }
        
        // DELETE api/news/4
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _newsService.DeleteNews(id);
        }
    }
}