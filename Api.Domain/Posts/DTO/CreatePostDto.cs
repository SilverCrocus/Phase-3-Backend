namespace Api.Domain.Posts.DTO;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
}