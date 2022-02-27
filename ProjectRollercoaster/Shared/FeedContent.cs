namespace ProjectRollercoaster.Shared
{
    using System.Collections.Generic;

    /// <summary>
    /// Information from both user and rss source.
    /// </summary>
    public class FeedContent
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Picture { get; set; }
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? PublishDate { get; set; }
        public List<string>? Links { get; set; }
    }
}