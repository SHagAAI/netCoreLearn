using System.Collections.Generic;
using System.Threading.Tasks;
using netCore.DTOs;
using NetCoreLearn.MyServiceResult;

namespace Services
{
    public interface IMovieService
    {
        Task<ServiceResult<IEnumerable<MovieResponseDto>>> GetAllMoviesAsync();
        Task<ServiceResult<MovieResponseDto>> GetMovieByIdAsync(int id);
        Task<ServiceResult<MovieResponseDto>> CreateMovieAsync(CreateMovieDto dto);
        Task<ServiceResult<MovieResponseDto>> UpdateMovieAsync(int id, UpdateMovieDto dto);
        Task<ServiceResult> DeleteMovieAsync(int id);
    }
}
