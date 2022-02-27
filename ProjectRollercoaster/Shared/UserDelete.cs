namespace ProjectRollercoaster.Shared
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Used when user tries to delete information from database.
    /// </summary>
    public class UserDelete
    {
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }
    }
}