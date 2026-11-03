using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetCoreLearn.DTOs;
using NetCoreLearn.Models;
using NetCoreLearn.MyServiceResult;

namespace NetCoreLearn.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterDto request)
        {
            // Check if email already exists (HTTP 409 Conflict)
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                return ServiceResult<AuthResponseDto>.Failure(
                    "An account with this email address already exists.",
                    statusCode: 409
                );
            }

            // 2. Check if username already exists (HTTP 409 Conflict)
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                return ServiceResult<AuthResponseDto>.Failure(
                    "An account with this username already exists.",
                    statusCode: 409
                );
            }

            // 3. Create the new ApplicationUser
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName
            };




            // 4. Hash password and save via Identity
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return ServiceResult<AuthResponseDto>.Failure(
                    $"Registration failed: {errors}",
                    statusCode: 400
                );
            }

            // ROLES
            var roleToAssign = string.Equals(request.Role, "Director", StringComparison.OrdinalIgnoreCase)
                ? "Director"
                : "User";

            await _userManager.AddToRoleAsync(user, roleToAssign);

            // 5. Retrieve roles, add it in generate JWT token for role claims
            var roles = await _userManager.GetRolesAsync(user);

            // 6. Generate JWT token
            var token = _tokenService.GenerateJwtToken(user, roles);


            var response = new AuthResponseDto
            {
                Token = token,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Roles = roles
            };

            return ServiceResult<AuthResponseDto>.Okay(
                response,
                "User registered successfully.",
                statusCode: 201
            );
        }

        public async Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto request)
        {
            // 1. Retrieve user by Username OR Email
            var user = await _userManager.FindByNameAsync(request.Username)
                       ?? await _userManager.FindByEmailAsync(request.Username);

            // 2. Validate user existence and password
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return ServiceResult<AuthResponseDto>.Failure(
                    "Invalid username or password.",
                    statusCode: 401
                );
            }

            var roles = await _userManager.GetRolesAsync(user);

            // 3. Generate token
            var token = _tokenService.GenerateJwtToken(user, roles);

            var response = new AuthResponseDto
            {
                Token = token,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Roles = roles
            };

            return ServiceResult<AuthResponseDto>.Okay(
                response,
                "Login successful.",
                statusCode: 200
            );
        }

        public async Task<ServiceResult> DeleteAccountAsync(string userId)
        {
            // 1. Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult.NotFound("User account was not found.");
            }

            // 2. Delete the user
            // (Microsoft Identity automatically deletes their role rows in AspNetUserRoles as well!)
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return ServiceResult.BadRequest($"Failed to delete account: {errors}");
            }

            return ServiceResult.Ok("User account deleted successfully.");
        }

    }
}