namespace MyBlog.Web.Models;

public sealed class BlogPost
{
    public required string Slug { get; init; }

    public required string Title { get; init; }

    public required string Summary { get; init; }

    public required DateOnly PublishedAt { get; init; }

    public required IReadOnlyList<string> Content { get; init; }
}
