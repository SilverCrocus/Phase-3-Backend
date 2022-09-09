using Api.Domain.Posts;
using Api.Domain.Posts.DTO;
using Api.Domain.Posts.Events;
using Api.Infrastructure.Persistence.Contexts;
using MediatR;

namespace Api.Services.Posts.Commands;

public class CreatePostCommand : IRequest
{
    public readonly CreatePostDto _dto;

    public CreatePostCommand(CreatePostDto dto)
    {
        _dto = dto;
    }
}

public class CreatePostHandler : IRequestHandler<CreatePostCommand, Unit>
{
    private readonly SqlDbContext _context;
    private readonly IMediator _mediator;

    public CreatePostHandler(SqlDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var dto = request._dto;
        
        var post = new Post
        {
            Title = dto.Title,
            Description = dto.Description,
            Content = dto.Content,
        };
        
        await _context.Post.AddAsync(post, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        await _mediator.Publish(new PostCreatedEvent{PostDto = dto}, cancellationToken);
        
        return Unit.Value;
    }
}