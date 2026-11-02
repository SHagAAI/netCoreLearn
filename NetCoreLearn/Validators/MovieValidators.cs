using FluentValidation;
using netCore.DTOs;

namespace netCore.Validators;

public class CreateMovieValidator : AbstractValidator<CreateMovieDto>
{
    public CreateMovieValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Movie title is required.")
            .Length(1, 200).WithMessage("Movie title must be between 1 and 200 characters.");

        RuleFor(x => x.ReleaseYear)
            .NotEmpty().WithMessage("Release year can't be empty.");

        RuleFor(x => x.DirectorId)
            .GreaterThan(0).WithMessage("A valid DirectorId is required.");

        RuleFor(x => x.GenreIds)
            .NotNull().WithMessage("GenreIds list cannot be null.");
    }
}

public class UpdateMovieValidator : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Movie title is required.")
            .Length(1, 200).WithMessage("Movie title must be between 1 and 200 characters.");

        RuleFor(x => x.ReleaseYear)
            .NotEmpty().WithMessage("Release year can't be empty.");

        RuleFor(x => x.DirectorId)
            .GreaterThan(0).WithMessage("A valid DirectorId is required.");

        RuleFor(x => x.GenreIds)
            .NotNull().WithMessage("GenreIds list cannot be null.");
    }
}
