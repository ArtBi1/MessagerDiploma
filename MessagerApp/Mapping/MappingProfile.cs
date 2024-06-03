using AutoMapper;
using MessagerApp.DTO;
using MessagerApp.Models;

namespace MessagerApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageViewModel>().ReverseMap();
        }
    }
}