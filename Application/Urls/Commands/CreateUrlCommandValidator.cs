using FluentValidation;

namespace Application.Urls.Commands;

public class CreateUrlCommandValidator : AbstractValidator<CreateUrlCommand>
{
    public CreateUrlCommandValidator()
    {
        RuleFor(x => x.CreatedBy)
            .NotEmpty()
            .WithMessage("Name is required.");
        
        RuleFor(x => x.OriginalUrl)
            .NotEmpty()
            .WithMessage("Url is required.");
    }
}