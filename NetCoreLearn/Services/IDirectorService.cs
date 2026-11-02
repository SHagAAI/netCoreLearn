using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreMovieCRUD.Models;
using netCore.DTOs;
using NetCoreLearn.MyServiceResult;

namespace Services
{
    public interface IDirectorService
    {
        Task<ServiceResult<IEnumerable<DirectorResponseDto>>> GetAllDirectorAsync();
        Task<ServiceResult<DirectorResponseDto>> GetDirectorByIdAsync(int id);
        Task<ServiceResult<DirectorResponseDto>> CreateDirectorAsync(CreateDirectorDto dto);
        Task<ServiceResult<DirectorResponseDto>> UpdateDirectorAsync(int id, UpdateDirectorDto dto);
        Task<ServiceResult> DeleteDirectorAsync(int id);
    }
}