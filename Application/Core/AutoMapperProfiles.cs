using Application.MovieRoom.DTO;
using Application.Movies.DTO;
using Application.Profile.DTO;
using Model;

namespace Application.Core;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Movie, MovieDTO>();
        
        CreateMap<Room, RoomDTO>()
            .ForMember(m => m.Movie, o => o.MapFrom(rm => rm.Movie));

        CreateMap<MovieDTO, Movie>();
        
        CreateMap<UpdateProfileDTO, UserApp>();
        
        CreateMap<UserApp, ProfileDTO>()
            .ForMember(o => o.FavoriteMovies, o => o.MapFrom(u => u.FavoriteMovies.Select(fm => fm.Movie)));

        CreateMap<UserRoom, AttendeesDTO>()
            .ForMember(u => u.UserName, o => o.MapFrom(a => a.User.UserName))
            .ForMember(u => u.DisplayName, o => o.MapFrom(a => a.User.DisplayName))
            .ForMember(o => o.Photo, o => o.MapFrom(u => u.User.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(i => i.IsHost, o => o.MapFrom(u => u.IsHost));


    }
}
