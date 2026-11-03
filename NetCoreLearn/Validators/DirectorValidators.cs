using FluentValidation;
using netCore.DTOs;

namespace netCore.Validators;

public class CreateDirectorValidator : AbstractValidator<CreateDirectorDto>
{
    public CreateDirectorValidator()
    {
        RuleFor(x => x.DirectorName)
            .NotEmpty()
            .WithMessage("Director name is required.")
            .Length(10, 100)
            .WithMessage("Director name must be between 10 and 100 characters.");
    }
}

public class UpdateDirectorValidator : AbstractValidator<UpdateDirectorDto>
{
    public UpdateDirectorValidator()
    {
        RuleFor(x => x.DirectorName)
            .NotEmpty()
            .WithMessage("Director name is required.")
            .Length(10, 100)
            .WithMessage("Director name must be between 2 and 100 characters.");
    }
}
