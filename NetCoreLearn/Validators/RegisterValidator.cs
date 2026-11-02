using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NetCoreLearn.DTOs;

namespace NetCoreLearn.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {


            RuleFor(x => x.FullName)
                    .NotEmpty()
                    .WithMessage("Full name is required.")
                    .MaximumLength(100)
                    .WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Username)
                    .NotEmpty()
                    .WithMessage("Username is required.")
                    .MinimumLength(6)
                    .WithMessage("Username must be at least 6 characters.")
                    .MaximumLength(50)
                    .WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email is required.")
                    .EmailAddress()
                    .WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Password is required.")
                    .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters long.");
        }

        public class LoginDtoValidator : AbstractValidator<LoginDto>
        {
            public LoginDtoValidator()
            {
                RuleFor(x => x.Username)
                    .NotEmpty()
                    .WithMessage("Username or email is required.");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Password is required.");
            }
        }
    }
}