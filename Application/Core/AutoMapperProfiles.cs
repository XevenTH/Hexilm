using Application.MovieRoom.DTO;
using Application.Profile.DTO;
using Model;

namespace Application.Core;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Movie, Movie>();
        
        CreateMap<Room, RoomDTO>();

        CreateMap<UserApp, ProfileDTO>();

        CreateMap<UserRoom, AttendeesDTO>()
            .ForMember(u => u.Username, o => o.MapFrom(a => a.User.UserName))
            .ForMember(u => u.DisplayName, o => o.MapFrom(a => a.User.DisplayName));
    }
}
