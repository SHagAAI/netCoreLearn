using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetCoreLearn.Models
{
    public class ApplicationUser : IdentityUser
    {
        //  IdentityUser
        // inherit all id, username,, email, password automatically
         public string FullName { get; set; } = string.Empty;
    }
}