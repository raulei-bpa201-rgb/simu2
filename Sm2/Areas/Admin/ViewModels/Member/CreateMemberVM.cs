using System.ComponentModel.DataAnnotations;

namespace Sm2.Areas.Admin.ViewModels.Member
{
    public class CreateMemberVM
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 symbol!")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 symbol!")]
        public string Title { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}