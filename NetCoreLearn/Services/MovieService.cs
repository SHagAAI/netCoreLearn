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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IDirectorRepository _directorRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;

        public MovieService(
            IMovieRepository movieRepo,
            IDirectorRepository directorRepo,
            IGenreRepository genreRepo,
            IMapper mapper)
        {
            _movieRepo = movieRepo;
            _directorRepo = directorRepo;
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<MovieResponseDto>>> GetAllMoviesAsync()
        {
            var movies = await _movieRepo.GetAllMoviesAsync();
            if (!movies.Any())
            {
                return ServiceResult<IEnumerable<MovieResponseDto>>.NotFound("No movies found in the database.");
            }

            var responseDtos = _mapper.Map<IEnumerable<MovieResponseDto>>(movies);
            return ServiceResult<IEnumerable<MovieResponseDto>>.Ok(responseDtos);
        }

        public async Task<ServiceResult<MovieResponseDto>> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepo.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return ServiceResult<MovieResponseDto>.NotFound($"Movie with ID {id} was not found.");
            }

            var responseDto = _mapper.Map<MovieResponseDto>(movie);
            return ServiceResult<MovieResponseDto>.Ok(responseDto);
        }

        public async Task<ServiceResult<MovieResponseDto>> CreateMovieAsync(CreateMovieDto dto)
        {
            // 1. Verify Director exists
            var director = await _directorRepo.GetDirectorByIdAsync(dto.DirectorId);
            if (director == null)
            {
                return ServiceResult<MovieResponseDto>.BadRequest($"Director with ID {dto.DirectorId} does not exist.");
            }

            // 2. Fetch Genres
            var genres = await _genreRepo.GetGenresByIdsAsync(dto.GenreIds);

            // 3. Map DTO -> Movie (Genres ignored by AutoMapper profile)
            var movie = _mapper.Map<Movie>(dto);
            movie.Genres = genres;

            await _movieRepo.InsertMovieAsync(movie);
            await _movieRepo.SaveChangesAsync();

            // Reload movie with navigation properties for complete response
            var createdMovie = await _movieRepo.GetMovieByIdAsync(movie.Id);
            var responseDto = _mapper.Map<MovieResponseDto>(createdMovie ?? movie);

            return ServiceResult<MovieResponseDto>.Ok(responseDto);
        }

        public async Task<ServiceResult<MovieResponseDto>> UpdateMovieAsync(int id, UpdateMovieDto dto)
        {
            var movie = await _movieRepo.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return ServiceResult<MovieResponseDto>.NotFound($"Movie with ID {id} was not found.");
            }

            // 1. Verify Director exists
            var director = await _directorRepo.GetDirectorByIdAsync(dto.DirectorId);
            if (director == null)
            {
                return ServiceResult<MovieResponseDto>.BadRequest($"Director with ID {dto.DirectorId} does not exist.");
            }

            // 2. Fetch Genres
            var genres = await _genreRepo.GetGenresByIdsAsync(dto.GenreIds);

            // 3. Update properties via AutoMapper
            _mapper.Map(dto, movie);
            movie.Genres = genres;

            _movieRepo.UpdateMovie(movie);
            await _movieRepo.SaveChangesAsync();

            var responseDto = _mapper.Map<MovieResponseDto>(movie);
            return ServiceResult<MovieResponseDto>.Ok(responseDto);
        }

        public async Task<ServiceResult> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepo.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return ServiceResult.NotFound($"Movie with ID {id} was not found.");
            }

            await _movieRepo.DeleteMovieAsync(id);
            await _movieRepo.SaveChangesAsync();

            return ServiceResult.Ok("Movie deleted successfully.");
        }
    }
}
