using System;
using System.Collections;
using System.Collections.Generic;
using BestinClass.Core.Application_Service.Impl;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET api/news
        [HttpGet]
        public ActionResult<IEnumerable<News>> Get([FromQuery] PageFilter filter)
        {
            try
            {
                if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
                {
                    var list = _newsService.GetAllNews(null);
                    return Ok(list);
                }
                else
                {
                    var list = _newsService.GetAllNews(filter);
                    return Ok(list);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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