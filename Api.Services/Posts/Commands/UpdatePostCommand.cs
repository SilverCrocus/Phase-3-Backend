using Api.Domain.Posts.DTO;
using Api.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Posts.Commands;

public class UpdatePostCommand : IRequest<Unit>
{
    public readonly int _id;
    public readonly UpdatePostDto _dto;

    public UpdatePostCommand(int id, UpdatePostDto dto)
    {
        _id = id;
        _dto = dto;
    }
}

public class UpdatePostHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly SqlDbContext _context;

    public UpdatePostHandler(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Post.FirstOrDefaultAsync(x => x.Id == request._id, cancellationToken: cancellationToken);
        
        if (post is null)
        {
            throw new Exception("Post not found");
        }
        
        post.Title = request._dto.Title;
        post.Description = request._dto.Description;
        post.Content = request._dto.Content;
        post.CreatedAt = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}