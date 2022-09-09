using Api.Domain.Posts;
using Api.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

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
    private readonly IMemoryCache _cache;

    public GetPostHandler(SqlDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Post> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(request._id, out Post post))
        {
            return post;
        }

        post = await _context.Post.FindAsync(request._id);
            
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1024);
            
        _cache.Set(request._id, post, cacheEntryOptions);

        if (post == null)
        {
            throw new Exception("Post not found");
        }

        return post;
    }
}