using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet("Set/{name}")]
        public void Set(string name)
        {
            _memoryCache.Set("name",$"{name}");
        }
        [HttpGet]
        public string Get()
        {
            if(_memoryCache.TryGetValue<string>("name", out string name))
            {
                return name.Substring(1, 3);
            }
            return "";
        } 

        [HttpDelete]
        public void Delete() 
        {
            _memoryCache.Remove("name");
        }

        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(7)
            });
        }

        [HttpGet("getDate")]

        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }



    }
}
