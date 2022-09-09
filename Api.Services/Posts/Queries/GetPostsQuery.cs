using Api.Domain.Posts;
using Api.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Posts.Queries;

public class GetPostsQuery : IRequest<IEnumerable<Post>>
{

}

public class GetPostsHandler : IRequestHandler<GetPostsQuery, IEnumerable<Post>>
{
    private readonly SqlDbContext _context;

    public GetPostsHandler(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        if (_context.Post == null) return new List<Post>();
        
        var posts = await _context.Post.ToListAsync(cancellationToken: cancellationToken);
        return posts;
    }
}