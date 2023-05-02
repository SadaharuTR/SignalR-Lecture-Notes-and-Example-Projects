using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Business;
using SignalRServerExample.Hubs;
using System.Threading.Tasks;

namespace SignalRServerExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //Öncelikli olarak MyBusiness'ı Dependency Injection ile talep etmeliyiz.
        readonly MyBusiness _myBusiness;
        readonly IHubContext<MyHub> _hubContext;
        public HomeController(MyBusiness myBusiness, IHubContext<MyHub> hubContext) //provider'dan gelen nesne,
        {
            _myBusiness = myBusiness; //referansa bağlanmış oldu.
            _hubContext = hubContext;
        }
        //bu nesneyi artık istediğimiz herhangi bir action'da kullanabiliriz.
        [HttpGet("{message}")]
        public async Task<IActionResult>Index(string message)
        {
            await _myBusiness.SendMessageAsync(message);
            return Ok();
        }
    }
}
