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

        [Required(ErrorMessage = "Please enter an valid rss link")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Please enter a name of your choice")]
        [StringLength(15, ErrorMessage = "Name is too long (max 15 characters")]
        public string? Name { get; set; }

        public User? User { get; set; }
    }
}