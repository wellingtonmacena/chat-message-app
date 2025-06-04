using ChatMessageWebApi.Models.Requests;
using ChatMessageWebApi.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ChatMessageWebApi.Services
{
    public sealed class ChatHub( IDistributedCache distributedCache, IUserService userService) : Hub<IChatClient>
    {
        private readonly IDistributedCache distributedCache = distributedCache;
        private readonly IUserService userService = userService;

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            HttpContext? httpContext = Context.GetHttpContext();
            Microsoft.Extensions.Primitives.StringValues? userId = httpContext?.Request.Headers["userId"];
            Dictionary<string, Microsoft.Extensions.Primitives.StringValues>? headers = httpContext?.Request.Headers.ToDictionary();

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
          .SetAbsoluteExpiration(DateTime.Now.AddHours(4));


            await distributedCache.SetStringAsync(userId, Context.ConnectionId, options, CancellationToken.None);
            await Clients.Client(Context.ConnectionId).ReceiveMessage($"{Context.ConnectionId} - Welcome to the chat! ");

        }

        public async Task SendMessage(string message)
        {
            HttpContext? httpContext = Context.GetHttpContext();
            Microsoft.Extensions.Primitives.StringValues? recipientIdHeader = httpContext?.Request.Headers["recipientId"];

            PostNewMessageRequest? messageJson = JsonSerializer.Deserialize<PostNewMessageRequest>(message);
            string? recipientId = await distributedCache.GetStringAsync(recipientIdHeader, CancellationToken.None);
            userService.CreateMessage(messageJson);


            Console.WriteLine($"Received message from {Context.ConnectionId}: {message}");
            //// Broadcast the message to all connected clients
            await Clients.Client(recipientId).ReceiveMessage($"{Context.ConnectionId}- {message} - gotback, bitch ");
        }
    }
}
