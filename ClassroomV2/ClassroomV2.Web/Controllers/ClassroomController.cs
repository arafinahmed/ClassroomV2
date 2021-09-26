using Autofac;
using ClassroomV2.Membership.Entities;
using ClassroomV2.Web.Models.Classroom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomV2.Web.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class ClassroomController : Controller
    {
        private readonly ILogger<ClassroomController> _logger;
        private readonly ILifetimeScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ClassroomController(ILogger<ClassroomController> logger, ILifetimeScope scope,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _scope = scope;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Classroom(int id)
        {
            var email = User.Identity.Name;
            var model = _scope.Resolve<LoadClassroom>();
            model.LoadClassRoomData(id, email);
            return View(model);
        }
        public IActionResult CreateClassroom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateClassroom(CreateClassroomModel model)
        {
            try
            {
                model.Resolve(_scope);
                model.AspUserId = Guid.Parse(_userManager.GetUserId(User));
                model.email = User.Identity.Name;
                model.CreateClassroom();
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index", "Classroom");
        }
        public IActionResult JoinClassroom()
        {
            var model = _scope.Resolve<CreateClassroomModel>();
            return View(model);
        }

        [HttpPost]
        public IActionResult JoinClassroom(int Id)
        {
            var email = User.Identity.Name;
            var model = _scope.Resolve<CreateClassroomModel>();
            model.JoinClassroom(Id, email);
            return View(model);
        }
        public JsonResult GetClasses()
        {
            var model = _scope.Resolve<CreateClassroomModel>();
            var email = User.Identity.Name;
            var data = model.GetClasses(email);
            return Json(data);
        }

        public JsonResult GetTeachers(int id)
        {
            var model = _scope.Resolve<LoadClassroom>();
            var email = User.Identity.Name;
            var data = model.GetTeachers(id);
            return Json(data);
        }
        public JsonResult GetStudents(int id)
        {
            var model = _scope.Resolve<LoadClassroom>();
            var email = User.Identity.Name;
            var data = model.GetStudents(id);
            return Json(data);
        }

        public IActionResult People(int userid)
        {
            var email = User.Identity.Name;
            var model = _scope.Resolve<LoadClassroom>();
            model.LoadClassRoomData(userid, email);
            return View(model);
        }
    }
}
