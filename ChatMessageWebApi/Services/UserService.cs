﻿using AutoMapper;
using ChatMessageWebApi.Models.DTOs;
using ChatMessageWebApi.Models.Entities;
using ChatMessageWebApi.Models.Requests;
using ChatMessageWebApi.Repositories.Interfaces;
using ChatMessageWebApi.Services.Interfaces;
using ChatMessageWebApi.Shared.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using posterr_webapi.src.Shared;

namespace ChatMessageWebApi.Services
{
    public class UserService(IDistributedCache distributedCache, IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<MessageDto> CreateMessage(PostNewMessageRequest postNewMessageRequest)
        {

            Message createdMessage = await _userRepository.CreateMessage(postNewMessageRequest);

            return _mapper.Map<MessageDto>(createdMessage);
        }

        public async Task<Paginate<MessageDto>> GetMessages(GetMessagesRequest getMessagesRequest)
        {
            Paginate<Message> messages = await _userRepository.GetMessages(getMessagesRequest);

            return _mapper.Map<Paginate<MessageDto>>(messages);
        }

        public async Task<List<UserDto>> GetUsers()
        {
            _logger.LogInformation("Getting all users.");

            List<User> users = await _userRepository.GetUsers();
            var usersDto =  _mapper.Map<List<UserDto>>(users);

            foreach (var item in usersDto)
            {
                item.IsOnline = !string.IsNullOrWhiteSpace( await distributedCache.GetStringAsync(item.Id.ToString(), CancellationToken.None));
            }

            return usersDto;
        }

        public async Task<UserDto> Login(string email, string password)
        {
            User user = await _userRepository.GetUserBy(email, password);

            if (user == null)
            {
                string message = "User not found with the provided email and password.";
                _logger.LogWarning("Login failed for user with email: {Email}", email);
                throw new UserNotFoundException(message);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
