using System.Collections.Generic;
using System.Threading.Tasks;
using netCore.DTOs;
using NetCoreLearn.MyServiceResult;

namespace Services
{
    public interface IGenreService
    {
        Task<ServiceResult<IEnumerable<GenreResponseDto>>> GetAllGenresAsync();
        Task<ServiceResult<GenreResponseDto>> GetGenreByIdAsync(int id);
        Task<ServiceResult<GenreResponseDto>> CreateGenreAsync(CreateGenreDto dto);
        Task<ServiceResult<GenreResponseDto>> UpdateGenreAsync(int id, UpdateGenreDto dto);
        Task<ServiceResult> DeleteGenreAsync(int id);
    }
}
