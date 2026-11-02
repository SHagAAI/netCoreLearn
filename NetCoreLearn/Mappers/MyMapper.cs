using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFCoreMovieCRUD.Models;
using netCore.DTOs;
using netCore.Models;

namespace NetCoreLearn.Mappers
{
    public class MyMapper : Profile
    {
        // 1. Director mappings


        public MyMapper()
        {
            // 1. Director Mapping
            CreateMap<CreateDirectorDto, Director>();
            CreateMap<UpdateDirectorDto, Director>();

            // Maps Database Entity -> Response DTO
            // for response
            CreateMap<Director, DirectorResponseDto>();



            // 2. Genre mappings
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();

            // 3. Movie mappings
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore()); // genres leave out, handled in service ? 
            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore());
            
            // for response ?
            CreateMap<Movie, MovieSummaryDto>();
        }
    }
}