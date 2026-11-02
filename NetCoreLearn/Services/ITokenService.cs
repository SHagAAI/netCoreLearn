using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreLearn.Models;

namespace NetCoreLearn.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}