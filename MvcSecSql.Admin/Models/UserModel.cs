using System.ComponentModel.DataAnnotations;

namespace MvcSecSql.Admin.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "User Id")]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Is Admin")]
        public bool IsAdmin { get; set; }
    }
}