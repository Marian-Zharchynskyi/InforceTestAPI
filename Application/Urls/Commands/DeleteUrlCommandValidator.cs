using FluentValidation;

namespace Application.Urls.Commands;

public class DeleteUrlCommandValidator : AbstractValidator<DeleteUrlCommand>
{
    public DeleteUrlCommandValidator()
    {
        RuleFor(x => x.UrlId)
            .NotEmpty()
            .WithMessage("Url is required.");
    }
}