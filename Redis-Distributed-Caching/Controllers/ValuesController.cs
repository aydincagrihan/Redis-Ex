using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Redis_Distributed_Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;
        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("set")]
        //Asenkron işlemler için Task kullandık IActionResultta controller returnları ıcın genel bir interfacedeir
        public async Task<IActionResult> Set(string name, string surname)
        {
            await _distributedCache.SetStringAsync("name", name, options: new()

            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)//5 saniye kullanmazsan cache'deki veri silinecek
            });
                
            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname));//SetAsync bytes turunden aldıgı ıcın donusturdum
            return Ok();
        }

        [HttpGet("get")]
        //Asenkron işlemler için Task kullandık IActionResultta controller returnları ıcın genel bir interfacedeir
        public async Task<IActionResult> Get()
        {
            var name = await _distributedCache.GetStringAsync("name");
            var surNameBinary = await _distributedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(surNameBinary);
            return Ok(
                new
                {
                    name,
                    surname
                });
        }
        /*
          IActionResult, ASP.NET Core’da bir controller metodunun döndürebileceği HTTP yanıtlarını yönetmek için kullanılır.
    ✅ Ok(), BadRequest(), NotFound(), Unauthorized(), StatusCode() gibi birçok yardımcı metot sağlar.
    ✅ Esneklik ve farklı HTTP durum kodları döndürme imkanı sunar.
    ✅ ActionResult<T> kullanarak daha tip güvenli (type-safe) dönüşler yapabilirsin.*/
    }
}
