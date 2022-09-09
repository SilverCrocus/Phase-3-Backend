using Api.Services.Posts.Commands;
using FluentValidation;

namespace Api.Services.Validation.Validators;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(x => x._id).NotEmpty()
            .WithMessage("Id is required");
    }
}