using AutoMapper;
using EFCoreMovieCRUD.Models;
using netCore.DTOs;
using netCore.Models;

namespace NetCoreLearn.Mappers
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            // 1. Director Mappings
            CreateMap<CreateDirectorDto, Director>();
            CreateMap<UpdateDirectorDto, Director>();
            
            CreateMap<Director, DirectorResponseDto>(); // for going out. for return ?

            // 2. Genre Mappings
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Genre, GenreResponseDto>();
            CreateMap<Genre, GenreSummaryDto>();

            // 3. Movie Mappings
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore());
            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore());

            CreateMap<Movie, MovieSummaryDto>();
            CreateMap<Movie, MovieResponseDto>()
                .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director != null ? src.Director.Name : string.Empty))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));
        }
    }
}
