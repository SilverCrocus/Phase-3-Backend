using Api.Services.Posts.Commands;
using FluentValidation;

namespace Api.Services.Validation.Validators;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x._dto.Title).NotEmpty().MaximumLength(255)
            .WithMessage("Title is required and cannot exceed 255 characters");
        
        RuleFor(x => x._dto.Description).MaximumLength(255)
            .WithMessage("Description cannot exceed 255 characters");
        
        RuleFor(x => x._dto.Content).NotEmpty()
            .WithMessage("Content is required");
    }
}