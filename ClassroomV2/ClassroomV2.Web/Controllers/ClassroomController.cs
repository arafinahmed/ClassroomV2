using Autofac;
using ClassroomV2.Membership.Entities;
using ClassroomV2.Web.Models.Classroom;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomV2.Web.Controllers
{
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
        public IActionResult Classroom()
        {
            return View();
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
    }
}
