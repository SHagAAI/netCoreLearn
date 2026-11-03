using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreLearn.DTOs;
using NetCoreLearn.MyServiceResult;

namespace NetCoreLearn.Services
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterDto request);
        Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto request);

        Task<ServiceResult> DeleteAccountAsync(string userId);
    }
}