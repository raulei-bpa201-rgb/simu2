using System.ComponentModel.DataAnnotations;

namespace Sm2.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Max 30 symbol!")]
        public string FullName { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Max 15 symbol!")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords must be same!")]
        public string ConfirmPassword { get; set; }
    }
}
