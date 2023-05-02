using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Business;
using SignalRServerExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRServerExample.Hubs
{
    public class MyHub : Hub<IMessageClient> 
    {
        static List<string> clients = new List<string>();

        //bu fonksiyon client'ların subscribe olacağı bir fonksiyondur.
        //Client Hub'a bir mesaj gönderecek, Server bu mesajı alıp gerekli işlemlerden sonra Hub üzerinden
        //diğer Client'lara gönderecektir. Server'ın yani Hub'ın mesajı karşılaması için string message
        //parametresi oluşturuldu.
        //Ne zaman tetiklenecek, o zaman Client'larda belirli fonksiyonlar ayağa kaldırılacaktır.
        //public async Task SendMessageAsync(string message)
        //{
        //    //Client'da receiveMessage isimli bir fonksiyon bekleniyor, o fonksiyonu tetikle, bunu yaparken
        //    //diğer client'ın gönderdiği yani buraya gelen mesajı message ile bu client'a gönder.
        //    await Clients.All.SendAsync("receiveMessage", message);
        //}
        //Sisteme bağlantı gerçekleştiğinde burası;
        public override async Task OnConnectedAsync()
        {
            //Kullanıcı-user join olduğu zaman client'lardaki userJoined fonksiyonunu tetikle ve
            //giriş yapan kullanıcının ConnectionId değerini client'lara gönder.
            clients.Add(Context.ConnectionId); //ekle ve
            //await Clients.All.SendAsync("clients", clients); //güncel halini client'lara gönder.
            await Clients.All.Clients(clients);
            //await Clients.All.SendAsync("userJoined", Context.ConnectionId);
            await Clients.All.UserJoined(Context.ConnectionId);
        }
        //bağlantı koptuğunda da burası tetiklenecektir.
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //Kullanıcı-user disconnect olduğu zaman client'lardaki userLeaved fonksiyonunu tetikle ve
            //çıkış yapan kullanıcının ConnectionId değerini client'lara gönder.
            clients.Remove(Context.ConnectionId); //sil ve
            //await Clients.All.SendAsync("clients", clients); //güncel halini client'lara gönder.
            await Clients.All.Clients(clients);
            //await Clients.All.SendAsync("userLeaved", Context.ConnectionId);
            await Clients.All.UserLeaved(Context.ConnectionId);
        }      
    }
}
