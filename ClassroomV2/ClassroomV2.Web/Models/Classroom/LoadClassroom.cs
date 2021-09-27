﻿using Autofac;
using ClassroomV2.Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomV2.Web.Models.Classroom
{
    public class LoadClassroom
    {
        public bool IsPermited { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }

        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public int ClassroomId { get; set; }

        public string StudentEmail { get; set; }
        public string TeacherEmail { get; set; }
        private IClassroomService _service;
        private ILifetimeScope _scope;
        public LoadClassroom() { }
        public LoadClassroom(IClassroomService service)
        {
            _service = service;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _service = _scope.Resolve<IClassroomService>();
        }

        internal void LoadClassRoomData(int id, string mail)
        {
            var res = _service.LoadClassRoomData(id, mail);
            IsPermited = res.isPermited;
            IsTeacher = res.isTeacher;
            IsStudent = !res.isTeacher;

            ClassroomName = res.Name;
            Description = res.Description;
            ClassroomId = res.Id;
        }

        internal object GetTeachers(int classroomId)
        {
            var data = _service.GetTeacherByClassId(classroomId);
            return new
            {
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = (from record in data
                        select new string[]
                        {
                            record.mail,
                            record.TeacherId.ToString()
                        }).ToArray()
            };
        }

        internal object GetStudents(int classroomId)
        {
            var data = _service.GetStudentByClassId(classroomId);
            return new
            {
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = (from record in data
                        select new string[]
                        {
                            record.mail,
                            record.StudentId.ToString()
                        }).ToArray()
            };
        }

        internal bool AddStudentToClass(int classid, string email)
        {
            var res = _service.JoinClassroom(classid, email);
            return res.x;
        }
        internal bool AddTeacherToClass(int classid, string email)
        {
            var res = _service.AddTeacherToClass(classid, email);
            return res.x;
        }
    }
}
