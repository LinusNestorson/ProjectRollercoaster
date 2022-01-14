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
        [Key]
        public int Id { get; set; }
        public string? RssLink { get; set; }
    }
}