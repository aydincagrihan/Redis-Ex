using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Redis_Ex_InMemory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Inject ediyoruz yani her valuesController cagırıldıgında IMemoryCache den bir instance oluşturulacaktır.
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        //[HttpGet("set/{name}")]
        //public void SetName(string name)
        //{
        //    _memoryCache.Set("name", name);
        //}

        //[HttpGet]
        //public string GetName()
        //{
        //    //bu if blogu setleme yapmadan önce get cagırılırsa hata ıle karsılasılıyor boyle bır senaryoyla boş bir deger gondererek hatadan kacmıs bulundum
        //    if (_memoryCache.TryGetValue<string>("name", out string name))
        //    {
        //        return name.Substring(2);//2.karakterden sonrakı tumunu getır
        //    }
        //    return "";
        //}
        [HttpGet("SetDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                //Cache'de ki datanın ne kadar tutulacagına daır net omrunun belırtılmesıdır.
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),//her 30 saniyede yeni bir tarih verisi cache'lensin istiyoruz
                //Cache'lenmiş datanın memory'de belirtilen sure periyodu zarfında tutulmasını belirtir
                SlidingExpiration = TimeSpan.FromSeconds(5)//5 saniye bu cache deki veri kullanılmazsa silinecektir bunu ayarlıyoruz.
            });
        }
        [HttpGet("GetDate")]


        public DateTime GetDate()
        {
         return   _memoryCache.Get<DateTime>("date");
        }
    }
}
