using System.Text.Json;

namespace GiftOfTheGivers.Web.HelpLibrary;

public sealed class HelpLibraryService
{
    private readonly IReadOnlyList<HelpArticle> _articles;

    public HelpLibraryService(IWebHostEnvironment environment)
    {
        var path = Path.Combine(environment.ContentRootPath, "HelpLibrary", "Articles", "help-library.json");
        if (!File.Exists(path))
        {
            _articles = [];
            return;
        }

        var json = File.ReadAllText(path);
        _articles = JsonSerializer.Deserialize<List<HelpArticle>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? [];
    }

    public IReadOnlyList<HelpArticle> GetAll() => _articles;

    public IReadOnlyList<HelpArticle> Search(string? query, string? category)
    {
        IEnumerable<HelpArticle> result = _articles;

        if (!string.IsNullOrWhiteSpace(category))
            result = result.Where(a => string.Equals(a.Category, category, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(query))
        {
            var q = query.Trim();
            result = result.Where(a =>
                a.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                a.Summary.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                a.Content.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                a.Tags.Any(t => t.Contains(q, StringComparison.OrdinalIgnoreCase)));
        }

        return result.OrderBy(a => a.Category).ThenBy(a => a.Title).ToList();
    }

    public HelpArticle? GetBySlug(string slug) =>
        _articles.FirstOrDefault(a => string.Equals(a.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<string> GetCategories() =>
        _articles.Select(a => a.Category).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x).ToList();
}
