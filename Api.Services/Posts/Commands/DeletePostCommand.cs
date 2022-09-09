using Api.Infrastructure.Persistence.Contexts;
using MediatR;

namespace Api.Services.Posts.Commands;

public class DeletePostCommand : IRequest<Unit>
{
    public readonly int _id;

    public DeletePostCommand(int id)
    {
        _id = id;
    }
}

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly SqlDbContext _dbContext;

    public DeletePostCommandHandler(SqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        // Get the post
        var post = await _dbContext.Post.FindAsync(request._id);
        
        // If the post is null, throw an exception
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        
        _dbContext.Post.Remove(post);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}