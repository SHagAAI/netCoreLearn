using FluentValidation;
using netCore.DTOs;

namespace netCore.Validators;

public class CreateGenreValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .Length(2, 50).WithMessage("Genre name must be between 2 and 50 characters.");
    }
}

public class UpdateGenreValidator : AbstractValidator<UpdateGenreDto>
{
    public UpdateGenreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .Length(2, 50).WithMessage("Genre name must be between 2 and 50 characters.");
    }
}
