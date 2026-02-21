using MyBlog.Web.Models;
using System.Text.Json;

namespace MyBlog.Web.Data;

public static class BlogPostsRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly object Sync = new();

    private static IReadOnlyList<BlogPost>? posts;

    private const string RelativePostsPath = "wwwroot/data/posts.json";

    public static IReadOnlyList<BlogPost> GetAll()
    {
        if (posts is not null)
        {
            return posts;
        }

        lock (Sync)
        {
            if (posts is not null)
            {
                return posts;
            }

            var jsonPath = ResolvePostsJsonPath();
            var json = File.ReadAllText(jsonPath);
            posts = JsonSerializer.Deserialize<List<BlogPost>>(json, JsonOptions) ?? [];

            return posts;
        }
    }

    public static BlogPost? GetBySlug(string slug)
    {
        return GetAll().FirstOrDefault(post => string.Equals(post.Slug, slug, StringComparison.OrdinalIgnoreCase));
    }

    private static string ResolvePostsJsonPath()
    {
        var candidatePaths = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), RelativePostsPath),
            Path.Combine(AppContext.BaseDirectory, RelativePostsPath)
        };

        foreach (var candidate in candidatePaths)
        {
            if (File.Exists(candidate))
            {
                return candidate;
            }
        }

        var current = new DirectoryInfo(AppContext.BaseDirectory);

        while (current is not null)
        {
            var candidate = Path.Combine(current.FullName, RelativePostsPath);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            current = current.Parent;
        }

        throw new FileNotFoundException($"Arquivo de posts não encontrado em '{RelativePostsPath}'.");
    }
}
