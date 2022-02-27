namespace ProjectRollercoaster.Shared
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Feed information from user.
    /// </summary>
    public class Feed
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter an valid rss link")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Please enter a name of your choice")]
        [StringLength(16, ErrorMessage = "Name is too long (max 16 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please choose an image that suits the feed")]
        public string? Image { get; set; }

        public User? User { get; set; }
    }
}