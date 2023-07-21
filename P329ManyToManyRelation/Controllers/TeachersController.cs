using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P329ManyToManyRelation.DAL;
using P329ManyToManyRelation.DAL.Entities;
using P329ManyToManyRelation.Models;

namespace P329ManyToManyRelation.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public TeachersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var teachers = _appDbContext.Teachers.Include(x => x.TeacherGroups).ThenInclude(x => x.Group).ToList();

            return View(teachers);
        }

        public IActionResult Create()
        {
            var groups = _appDbContext.Groups.ToList();
            var groupsForViewModel = new List<SelectListItem>();

            groups.ForEach(x => groupsForViewModel.Add(new SelectListItem(x.Name, x.Id.ToString())));
            var model = new TeacherCreateViewModel
            {
                Groups = groupsForViewModel,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeacherCreateViewModel model)
        {
            var groups = _appDbContext.Groups.ToList();
            var groupsForViewModel = new List<SelectListItem>();

            groups.ForEach(x => groupsForViewModel.Add(new SelectListItem(x.Name, x.Id.ToString())));
            var viewModel = new TeacherCreateViewModel
            {
                Groups = groupsForViewModel
            };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var teacher = new Teacher
            {
                Name = model.Name
            };

            var teacherGroups = new List<TeacherGroup>();

            foreach (var groupId in model.GroupIds)
            {
                teacherGroups.Add(new TeacherGroup
                {
                    TeacherId = teacher.Id,
                    GroupId = groupId
                });
            }

            teacher.TeacherGroups = teacherGroups;

            _appDbContext.Teachers.Add(teacher);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var teacher = _appDbContext.Teachers.Include(x => x.TeacherGroups).ThenInclude(x=>x.Group).FirstOrDefault(x => x.Id == id);

            var groupsForViewModel = new List<SelectListItem>();

            teacher.TeacherGroups.ToList().ForEach(x => groupsForViewModel.Add(new SelectListItem(x.Group.Name, x.Group.Id.ToString())));
            var viewModel = new TeacherUpdateViewModel
            {
                Name = teacher.Name,
                RemovedGroups = groupsForViewModel
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, TeacherUpdateViewModel model)
        {
            var teacher = _appDbContext.Teachers.Include(x => x.TeacherGroups).FirstOrDefault(x => x.Id == id);

            var teacherGroups = teacher.TeacherGroups.ToList();

            var newTeacherGroups = new List<TeacherGroup>();

            foreach (var item in teacherGroups)
            {
                if(!model.RemovedGroupIds.Contains(item.GroupId))
                    newTeacherGroups.Add(item);
            }

            teacher.TeacherGroups = newTeacherGroups;

            _appDbContext.Teachers.Update(teacher);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
