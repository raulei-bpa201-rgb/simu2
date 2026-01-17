using Microsoft.AspNetCore.Mvc;
using Sm2.DAL;
using Sm2.Models;
namespace Sm2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Member> members =_context.TeamMembers.ToList();
            
            return View(members);
        }
    }
}
