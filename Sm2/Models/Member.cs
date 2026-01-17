using Microsoft.AspNetCore.Mvc;
using Sm2.Models.Base;
namespace Sm2.Models
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
