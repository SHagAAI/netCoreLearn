using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using netCore.DTOs;
using netCore.Models;
using NetCoreLearn.MyServiceResult;
using NetCoreLearn.Repository;

namespace Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepo, IMapper mapper)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<GenreResponseDto>>> GetAllGenresAsync()
        {
            var genres = await _genreRepo.GetAllGenresAsync();
            if (!genres.Any())
            {
                return ServiceResult<IEnumerable<GenreResponseDto>>.NotFound("No genres found in the database.");
            }

            var responseDtos = _mapper.Map<IEnumerable<GenreResponseDto>>(genres);
            return ServiceResult<IEnumerable<GenreResponseDto>>.Ok(responseDtos);
        }

        public async Task<ServiceResult<GenreResponseDto>> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepo.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return ServiceResult<GenreResponseDto>.NotFound($"Genre with ID {id} was not found.");
            }

            var responseDto = _mapper.Map<GenreResponseDto>(genre);
            return ServiceResult<GenreResponseDto>.Ok(responseDto);
        }

        public async Task<ServiceResult<GenreResponseDto>> CreateGenreAsync(CreateGenreDto createGenreDto)
        {
            var genre = _mapper.Map<Genre>(createGenreDto);

            await _genreRepo.InsertGenreAsync(genre);
            await _genreRepo.SaveChangesAsync();

            var responseDto = _mapper.Map<GenreResponseDto>(genre);
            return ServiceResult<GenreResponseDto>.Ok(responseDto);
        }

        public async Task<ServiceResult<GenreResponseDto>> UpdateGenreAsync(int id, UpdateGenreDto updateGenreDto)
        {
            var genre = await _genreRepo.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return ServiceResult<GenreResponseDto>.NotFound($"Genre with ID {id} was not found.");
            }

            _mapper.Map(updateGenreDto, genre);
            _genreRepo.UpdateGenre(genre);
            await _genreRepo.SaveChangesAsync();

            var responseDto = _mapper.Map<GenreResponseDto>(genre);
            return ServiceResult<GenreResponseDto>.Ok(responseDto);
        }

        public async Task<ServiceResult> DeleteGenreAsync(int id)
        {
            var genre = await _genreRepo.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return ServiceResult.NotFound($"Genre with ID {id} was not found.");
            }

            await _genreRepo.DeleteGenreAsync(id);
            await _genreRepo.SaveChangesAsync();

            return ServiceResult.Ok($"Genre with ID {id} was deleted successfully.");
        }
    }
}
