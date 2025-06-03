using AutoMapper;
using ChatMessageWebApi.Models.DTOs;
using ChatMessageWebApi.Models.Entities;
using posterr_webapi.src.Shared;

namespace ChatMessageWebApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<Post, PostDto>().ReverseMap();
            //CreateMap<Repost, RepostDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Paginate<Message>, Paginate<MessageDto>>().ReverseMap();
            CreateMap<Message,MessageDto>().ReverseMap();

        }
    }
}
