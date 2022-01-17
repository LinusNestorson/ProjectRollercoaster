namespace ProjectRollercoaster.Shared
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Feed
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Link { get; set; }
        public string? PublishDate { get; set; }
        public User? User { get; set; }
    }
}