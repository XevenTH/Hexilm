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
            .ForMember(u => u.Username, o => o.MapFrom(a => a.User.UserName))
            .ForMember(u => u.DisplayName, o => o.MapFrom(a => a.User.DisplayName));
    }
}
