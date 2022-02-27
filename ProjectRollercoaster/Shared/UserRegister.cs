namespace ProjectRollercoaster.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Used when user tries to login.
    /// </summary>
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [StringLength(16, ErrorMessage = "Username is too long (max 16 characters)")]
        public string Username { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please confirm")]
        public bool IsConfirmed { get; set; } = true;
    }
}