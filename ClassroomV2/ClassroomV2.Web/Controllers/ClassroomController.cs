using Autofac;
using ClassroomV2.Common.Utilities;
using ClassroomV2.Membership.Entities;
using ClassroomV2.Web.Models.Classroom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        private readonly IEmailService _emailService;

        public ClassroomController(ILogger<ClassroomController> logger, ILifetimeScope scope,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService)
        {
            _logger = logger;
            _scope = scope;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
            model.GetPosts();
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
                var classId = model.CreateClassroom();
                return RedirectToAction("Classroom", "Classroom", new { id = classId});
            }
            catch
            {
                return View();
            }
        }
        public IActionResult JoinClassroom()
        {
            var model = _scope.Resolve<CreateClassroomModel>();
            return View(model);
        }

        [HttpPost]
        public IActionResult JoinClassroom(int id)
        {
            var email = User.Identity.Name;
            var model = _scope.Resolve<CreateClassroomModel>();
            model.JoinClassroom(id, email);
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

        public IActionResult People(int id)
        {
            var email = User.Identity.Name;
            var model = _scope.Resolve<LoadClassroom>();
            model.LoadClassRoomData(id, email);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddStudent(LoadClassroom model)
        {
            model.Resolve(_scope);
            bool response = model.AddStudentToClass(model.ClassroomId, model.StudentEmail);
            if (response)
            {
                _emailService.SendEmail(model.StudentEmail, "Join to your Class | " + model.ClassroomName ,
                        $"Please login to your account by <a href='localhost'>clicking here</a>.");
            }
            return RedirectToAction("People", "Classroom", new { id = model.ClassroomId });
        }
        [HttpPost]
        public IActionResult AddTeacher(LoadClassroom model)
        {
            model.Resolve(_scope);
            bool response = model.AddTeacherToClass(model.ClassroomId, model.TeacherEmail);
            if (response)
            {
                _emailService.SendEmail(model.TeacherEmail, "Welcome to " + model.ClassroomName,
                        $"Please login to your account by <a href='localhost'>clicking here</a>." +
                        $"<div><b>We hope your teaching will be very helpfull for our student.</b></div>");
            }
            return RedirectToAction("People", "Classroom", new { id = model.ClassroomId });
        }

        public IActionResult CreatePost(int id)
        {
            var email = User.Identity.Name;
            var model = _scope.Resolve<LoadClassroom>();
            model.LoadClassRoomData(id, email);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(LoadClassroom model)
        {
            var email = User.Identity.Name;
            model.Resolve(_scope);
            model.LoadClassRoomData(model.ClassroomId, email);
            if (ModelState.IsValid)
            {
                try
                {
                    if(model.PostFormFile != null)
                    {
                        var filename = ContentDispositionHeaderValue.Parse(model.PostFormFile.ContentDisposition).FileName.Trim('"');
                        model.PostFileName = filename;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploded", Guid.Parse(_userManager.GetUserId(User)).ToString() + model.PostFormFile.FileName);
                        model.PostFilePath = path;
                        using (Stream stream = new FileStream(path, FileMode.Create))
                        {
                            await model.PostFormFile.CopyToAsync(stream);
                        }
                    }
                    model.PostUpload();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, "Failed in Upload");
                    return View(model);
                }
                return RedirectToAction("Classroom", "Classroom", new { id = model.ClassroomId });
            }
            return View(model);
        }
        public FileResult DownloadFile(string path, string fileName)
        {
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        public IActionResult SendEmail(string email, int classId)
        {
            var UserEmail = User.Identity.Name;
            var model = _scope.Resolve<LoadClassroom>();
            model.LoadClassRoomData(classId, email);
            var sendMailModel = new SendMailModel {
                ReceiverMail = email,
                SenderMail = UserEmail,
                ClassId = classId,
                Subject = "Via " + model.ClassroomName
            };

            return View(sendMailModel);
        }
        [HttpPost]
        public IActionResult SendEmail(SendMailModel model)
        {
            string messageBody = $"<p> You have a message new from <b> {model.SenderMail} </b></p>" +
                $"<p>------------------------------</p>" +
                $"<p>{model.Body}</p>"+
                $"<p>------------------------------</p> " +
                $"<p  ~   </p> " +
                $"<p>Thank for using EduRoom </p>" ;
            _emailService.SendEmail(model.ReceiverMail, model.Subject, messageBody);
            return RedirectToAction("Classroom", "Classroom", new { id = model.ClassId });
        }
    }
}
