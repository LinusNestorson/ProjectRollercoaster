namespace ProjectRollercoaster.Shared
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserDelete
    {
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }
    }
}