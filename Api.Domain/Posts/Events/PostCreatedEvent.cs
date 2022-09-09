using Api.Domain.Posts.DTO;
using MediatR;

namespace Api.Domain.Posts.Events;

public class PostCreatedEvent : INotification
{
    public CreatePostDto PostDto { get; set; }
}