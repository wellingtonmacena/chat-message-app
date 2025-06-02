using AutoMapper;
using ChatMessageWebApi.Models.DTOs;
using ChatMessageWebApi.Models.Entities;

namespace ChatMessageWebApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<Post, PostDto>().ReverseMap();
            //CreateMap<Repost, RepostDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
