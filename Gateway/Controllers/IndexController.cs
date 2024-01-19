using System;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        // GET: api/values
        [HttpGet]
        public ResultMessage Get()
        {
            var result = new ResultMessage();
            return result;
        }

        public class ResultMessage
        {
            public string Message { get; set; } = "Connected 1";
        }
    }
}

