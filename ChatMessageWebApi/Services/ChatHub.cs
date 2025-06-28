using ChatMessageWebApi.Models.Requests;
using ChatMessageWebApi.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ChatMessageWebApi.Services
{
    public sealed class ChatHub(IDistributedCache distributedCache, IUserService userService) : Hub<IChatClient>
    {
        private readonly IDistributedCache distributedCache = distributedCache;
        private readonly IUserService userService = userService;


        public override async Task OnConnectedAsync()
        {

            HttpContext? httpContext = Context.GetHttpContext();
            Microsoft.Extensions.Primitives.StringValues userId = Context.GetHttpContext().Request.Query["userId"];
            Console.WriteLine($"Client connected: {Context.ConnectionId} - UserId:{userId}");
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
          .SetAbsoluteExpiration(DateTime.Now.AddHours(4));


            await distributedCache.SetStringAsync(userId, Context.ConnectionId, options, CancellationToken.None);
            await distributedCache.SetStringAsync( Context.ConnectionId, userId, options, CancellationToken.None);
            await Clients.Client(Context.ConnectionId).ReceiveMessage($"{Context.ConnectionId} - Welcome to the chat! ");

            await base.OnConnectedAsync();

        }

        public async Task SendMessage(string message)
        {
            HttpContext? httpContext = Context.GetHttpContext();

            PostNewMessageRequest? messageJson = JsonSerializer.Deserialize<PostNewMessageRequest>(message);
            string? recipientId = await distributedCache.GetStringAsync(messageJson.RecipientId.ToString(), CancellationToken.None);
            Models.DTOs.MessageDto createdMessage = await userService.CreateMessage(messageJson);


            Console.WriteLine($"Received message from {Context.ConnectionId}: {message}");
            //// Broadcast the message to all connected clients

            if (!string.IsNullOrWhiteSpace(recipientId))
                await Clients.Client(recipientId).ReceiveMessage(JsonSerializer.Serialize(createdMessage));

            //await Clients.Client(Context.ConnectionId).ReceiveMessage($"{Context.ConnectionId} - Received ");
            //await Clients.All.ReceiveMessage($"{Context.ConnectionId}- {message} - gotback  ");

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userId = await distributedCache.GetStringAsync(Context.ConnectionId, CancellationToken.None);
            await distributedCache.RemoveAsync(Context.ConnectionId, CancellationToken.None);
            if(!string.IsNullOrWhiteSpace(userId))
                await distributedCache.RemoveAsync(userId, CancellationToken.None);
            

            await Clients.All.ReceiveMessage(JsonSerializer.Serialize(userId));
            
        }
    }
}
