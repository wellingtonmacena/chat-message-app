using ChatMessageWebApi.Models.DTOs;
using ChatMessageWebApi.Models.Requests;
using ChatMessageWebApi.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using posterr_webapi.src.Shared;

namespace ChatMessageWebApi.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class UsersController(ILogger<UsersController> logger, IUserService userService)
    {

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            Result<List<UserDto>> output = new();
            output.WithValue(await userService.GetUsers());

            return output.ToActionResult();

        }

        [HttpPost("users/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            Result<UserDto> output = new();
            output.WithValue(await userService.Login(loginRequest.Email, loginRequest.Password));

            return output.ToActionResult();

        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages([FromQuery] GetMessagesRequest getMessagesRequest)
        {
            Result<Paginate<MessageDto>> output = new();
            output.WithValue(await userService.GetMessages(getMessagesRequest));

            return output.ToActionResult();

        }

        [HttpPost("messages")]
        public async Task<IActionResult> CreateMessage([FromBody] PostNewMessageRequest postNewMessageRequest)
        {
            Result<MessageDto> output = new();
            output.WithValue(await userService.CreateMessage(postNewMessageRequest));

            return output.ToActionResult();

        }

    }
}
