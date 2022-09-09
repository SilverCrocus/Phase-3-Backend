using Api.Domain.Posts;
using Api.Infrastructure.Persistence.Contexts;
using MediatR;

namespace Api.Services.Posts.Queries;

public class GetPostQuery : IRequest<Post>
{
    public readonly int _id;

    public GetPostQuery(int id)
    {
        _id = id;
    }
}

public class GetPostHandler : IRequestHandler<GetPostQuery, Post>
{
    private readonly SqlDbContext _context;

    public GetPostHandler(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<Post> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _context.Post.FindAsync(request._id);
        
        if (post == null)
        {
            throw new Exception("Post not found");
        }

        return post;
    }
}