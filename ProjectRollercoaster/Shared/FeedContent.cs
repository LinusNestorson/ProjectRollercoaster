namespace ProjectRollercoaster.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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