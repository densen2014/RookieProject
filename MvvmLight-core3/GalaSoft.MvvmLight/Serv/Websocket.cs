using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1.Serv
{
    class Websocket
    {
       async  public Task Server() 
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();
            var context = await listener.GetContextAsync();
            var wsContext = await context.AcceptWebSocketAsync(null);
            var ws = wsContext.WebSocket;
         }

        async public Task Client()
        {
            var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri("ws://127.0.0.1:8080"), CancellationToken.None);
        }

    }
}
