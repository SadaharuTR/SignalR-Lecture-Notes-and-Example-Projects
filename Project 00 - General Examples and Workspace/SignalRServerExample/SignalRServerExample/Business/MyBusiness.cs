using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Hubs;
using System.Threading.Tasks;

namespace SignalRServerExample.Business
{
    public class MyBusiness
    {
        //bu referans sayesinde artık bu normal class'ta WebSocket işlemlerimizi gerçekleştirebileceğiz.
        readonly IHubContext<MyHub> _hubContext;
        //gelen nesneyi de aşağıdaki gibi injekte edebiliriz.
        public MyBusiness(IHubContext<MyHub> hubContext) 
        { 
            _hubContext = hubContext; 
        }
        public async Task SendMessageAsync(string message)
        {           
            await _hubContext.Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
