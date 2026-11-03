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
            //          SOURCE         , DESTINATION
            CreateMap<CreateDirectorDto, Director>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src=>src.DirectorName));

                       
            CreateMap<UpdateDirectorDto, Director>()
                        .ForMember(dest=> dest.Name, opt => opt.MapFrom(src => src.DirectorName));
            
            CreateMap<Director, DirectorResponseDto>()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // for going out. for return ?
                            .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));

            // 2. Genre Mappings
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Genre, GenreResponseDto>();
            CreateMap<Genre, GenreSummaryDto>();

            // 3. Movie Mappings
            // ignore genre
            // the rest map automatically
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore());

            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore());

            // data from db, all matched
            CreateMap<Movie, MovieSummaryDto>();

            CreateMap<Movie, MovieResponseDto>()
                .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director != null ? src.Director.Name : string.Empty))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));
        }
    }
}
