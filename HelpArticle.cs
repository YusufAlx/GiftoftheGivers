namespace GiftOfTheGivers.Web.HelpLibrary;

public sealed class HelpArticle
{
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string[] Tags { get; set; } = [];
    public string Summary { get; set; } = "";
    public string Content { get; set; } = "";
}
