namespace ProjectRollercoaster.Shared
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Podcast
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter an valid rss link")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Please enter a name of your choice")]
        [StringLength(20, ErrorMessage = "Name is too long (max 20 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please choose an image that suits the feed")]
        public string? Image { get; set; }

        public User? User { get; set; }
    }
}