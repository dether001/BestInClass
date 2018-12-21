using System;
using System.Collections.Generic;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        //GET api/user
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<LoginInputModel> Post([FromBody] LoginInputModel model)
        {
            _userService.RegisterUser(model.Username, model.Password);
            return Ok("Successful registration !");
        }
    }
}