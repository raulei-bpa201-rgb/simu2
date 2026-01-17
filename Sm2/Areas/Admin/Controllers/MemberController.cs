using Microsoft.AspNetCore.Mvc;
using Sm2.Areas.Admin.ViewModels.Member;
using Sm2.DAL;
using Sm2.Models;
using Sm2.Utilities.Enums;
using Sm2.Utilities.Extensions;
using Sm2.DAL;

namespace Sm2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MemberController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Member> members = _context.TeamMembers.ToList();
            return View(members);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberVM CreateMemberVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!CreateMemberVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be an image!");
                return View();
            }
            if (CreateMemberVM.Photo.ValidateSize(2, FileSize.MB))
            {
                ModelState.AddModelError("Photo", "Maximum file size is 2 MB!");
                return View();
            }
            Member member = new Member()
            {
                Name = CreateMemberVM.Name,
                Title = CreateMemberVM.Title,
                Image = await CreateMemberVM.Photo.CreateFile(_env.WebRootPath, "img")
            };
            await _context.AddRangeAsync(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Member members = _context.TeamMembers.FirstOrDefault(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(members);
            }
            UpdateMemberVM vm = new UpdateMemberVM()
            {
                Name = members.Name,
                Title = members.Title,
                Image = members.Image
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateMemberVM UpdateMemberVM)
        {
            if (!ModelState.IsValid)
            {
                return View(UpdateMemberVM);
            }
            Member members = _context.TeamMembers.FirstOrDefault(m => m.Id == id);
            if (UpdateMemberVM.Photo is not null)
            {
                if (!UpdateMemberVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be an image!");
                    return View();
                }
                if (UpdateMemberVM.Photo.ValidateSize(2, FileSize.MB))
                {
                    ModelState.AddModelError("Photo", "Maximum file size is 2 MB!");
                    return View();
                }
                string fileName = await UpdateMemberVM.Photo.CreateFile(_env.WebRootPath, "img");
                members.Image = fileName;
            }
            members.Name = UpdateMemberVM.Name;
            members.Title = UpdateMemberVM.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int? id)
        {
            Member members = _context.TeamMembers.FirstOrDefault(members => members.Id == id);
            return View(members);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Member members = _context.TeamMembers.FirstOrDefault(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(members);
            }
            members.Image.DeleteFile(_env.WebRootPath, "img");
            _context.TeamMembers.Remove(members);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}