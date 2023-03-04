using Application.MovieRoom.DTO;
using AutoMapper;
using Model;

namespace Application.Core;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Room, RoomDTO>();

        CreateMap<UserRoom, AttendeesDTO>()
            .ForMember(u => u.Id, o => o.MapFrom(a => a.User.Id))
            .ForMember(u => u.Username, o => o.MapFrom(a => a.User.UserName))
            .ForMember(u => u.Displayname, o => o.MapFrom(a => a.User.DisplayName));
    }
}
