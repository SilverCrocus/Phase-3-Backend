using Api.Domain.Posts.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.RealTime;

public class PostEventsClientDispatcher : INotificationHandler<PostCreatedEvent>
{
    private readonly IHubContext<PostEventsClientHub> _hubContext;

    public PostEventsClientDispatcher(IHubContext<PostEventsClientHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task Handle(PostCreatedEvent @event, CancellationToken cancellationToken)
    {
        return _hubContext.Clients.All.SendAsync("postCreated", @event, cancellationToken);
    }
}