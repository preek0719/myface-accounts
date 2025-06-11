using System.ComponentModel.DataAnnotations;

namespace MyFace.Models.Request
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(70)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(70)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(70)]
        public string Username { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 12, ErrorMessage = "Password must be at least 12 characters")]
        // [RegularExpression(@"^.(?=.{12,})(?=.[a-zA-Z])(?=.\d)(?=.[!&$%&?]).$", ErrorMessage = "Password must be 12+ characters, and contain one upper case letter, one lower case letter, one number and one of !&$%&?")]
        public string Password { get; set; }
        
        public string ProfileImageUrl { get; set; }
        
        public string CoverImageUrl { get; set; }
    }
}