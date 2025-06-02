using ChatMessageWebApi.Models.DTOs;
using ChatMessageWebApi.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using posterr_webapi.src.Shared;

namespace ChatMessageWebApi.Controllers
{
    [ApiController]
    public class UsersController(ILogger<UsersController> logger, IUserService userService)
    {

        [HttpGet("/users")]
        public async Task<IActionResult> GetUsers()
        {
            Result<List<UserDto>> output = new();
            output.WithValue(await userService.GetUsers());

            return output.ToActionResult();

        }

        [HttpGet("/messages")]
        public async Task<IActionResult> GetMessages()
        {
            Result<List<UserDto>> output = new();
            output.WithValue(await userService.GetUsers());

            return output.ToActionResult();

        }

        [HttpGet("/conversations")]
        public async Task<IActionResult> GetConversations()
        {
            Result<List<UserDto>> output = new();
            output.WithValue(await userService.GetUsers());

            return output.ToActionResult();

        }

        [HttpPost("/messages")]
        public async Task<IActionResult> CreateMessage()
        {
            Result<List<UserDto>> output = new();
            output.WithValue(await userService.GetUsers());

            return output.ToActionResult();

        }

        
    }
}
