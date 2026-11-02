using FluentValidation;
using netCore.DTOs;

namespace netCore.Validators;

public class CreateDirectorValidator : AbstractValidator<CreateDirectorDto>
{
    public CreateDirectorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Director name is required.")
            .Length(2, 100).WithMessage("Director name must be between 2 and 100 characters.");
    }
}

public class UpdateDirectorValidator : AbstractValidator<UpdateDirectorDto>
{
    public UpdateDirectorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Director name is required.")
            .Length(2, 100).WithMessage("Director name must be between 2 and 100 characters.");
    }
}
