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
            CreateMap<Message, MessageDto>()
    .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Conversation.SenderId))
    .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.Conversation.RecipientId))
    .ReverseMap()
    .ForPath(src => src.Conversation.SenderId, opt => opt.MapFrom(dest => dest.SenderId))
    .ForPath(src => src.Conversation.RecipientId, opt => opt.MapFrom(dest => dest.RecipientId));

        }
    }
}
