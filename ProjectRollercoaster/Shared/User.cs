namespace ProjectRollercoaster.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Storing user information used for communication with database.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? Email { get; set; }
        public string? Username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}