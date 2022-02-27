namespace ProjectRollercoaster.Shared
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Used when user tries to login.
    /// </summary>
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter an Email Adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }
    }
}