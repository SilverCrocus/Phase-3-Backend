using System.Net.WebSockets;
using Api.Domain.Posts.DTO;
using Api.Services.Posts.Commands;
using Api.Services.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetPosts()
    {
        var posts = await _mediator.Send(new GetPostsQuery());

        return Ok(posts);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetPostById(int id)
    {
        var post = await _mediator.Send(new GetPostQuery(id));

        return Ok(post);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreatePost([FromBody] CreatePostDto post)
    {
        var createdPost = await _mediator.Send(new CreatePostCommand(post));

        return Ok(createdPost);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost(int id, [FromBody] UpdatePostDto post)
    {
        var updatedPost = await _mediator.Send(new UpdatePostCommand(id, post));

        return Ok(updatedPost);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        await _mediator.Send(new DeletePostCommand(id));

        return Ok();
    }
}