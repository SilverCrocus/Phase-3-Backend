using Api.Domain.Posts;
using Api.Domain.Posts.DTO;
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

    public CreatePostHandler(SqlDbContext context)
    {
        _context = context;
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
        
        return Unit.Value;
    }
}