using System.ComponentModel.DataAnnotations;

namespace Sm2.Areas.Admin.ViewModels.Member
{
    public class UpdateMemberVM
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 symbol!")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 symbol!")]
        public string Title { get; set; }

        public string Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}