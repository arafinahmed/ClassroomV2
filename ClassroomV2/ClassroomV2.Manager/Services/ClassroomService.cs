using AutoMapper;
using ClassroomV2.Manager.BusinessObjects;
using ClassroomV2.Manager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IManagerUnitOfWorks _unitOfWork;
        private readonly IMapper _mapper;
        public ClassroomService(IManagerUnitOfWorks unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public int CreateClassroom(Classroom classroom)
        {
            if (classroom == null)
                throw new InvalidOperationException("no Classroom is provided");
            try
            {
                var entity = _mapper.Map<Entities.Classroom>(classroom);
                _unitOfWork.Classroom.Add(entity);
                _unitOfWork.Save();
                _unitOfWork.ClassUser.Add(new Entities.ClassUser { ClassId = entity.Id, email = classroom.Email });
                _unitOfWork.Teacher.Add(new Entities.Teacher { ClassroomId = entity.Id, email = classroom.Email });
                _unitOfWork.Save();
                return entity.Id;
            }
            catch
            {
                throw new Exception("Entity not added");
            }

        }

        public (bool x, string message) JoinClassroom(int classId, string email)
        {
            var count = _unitOfWork.ClassUser.GetCount(x => x.ClassId == classId && x.email == email);
            if(count != 0)
            {
                return (false, "Already in this class");
            }
            count = _unitOfWork.Classroom.GetCount(x => x.Id == classId);
            if(count ==0)
            {
                return (false, "No classroom found");
            }
            _unitOfWork.ClassUser.Add(new Entities.ClassUser { ClassId = classId, email = email });
            _unitOfWork.Student.Add(new Entities.Student { ClassroomId = classId, email = email });
            _unitOfWork.Save();
            return (true, "Successfull");
        }
        public (bool x, string message) AddTeacherToClass(int classId, string email)
        {
            var count = _unitOfWork.Teacher.GetCount(x => x.ClassroomId == classId && x.email == email);
            if (count != 0)
            {
                return (false, "Already teacher in this class");
            }
            count = _unitOfWork.Classroom.GetCount(x => x.Id == classId);
            if (count == 0)
            {
                return (false, "No classroom found");
            }
            _unitOfWork.Teacher.Add(new Entities.Teacher { ClassroomId = classId, email = email });
            count = _unitOfWork.ClassUser.GetCount(x => x.ClassId == classId && x.email == email);
            if (count == 0)
            {
                _unitOfWork.ClassUser.Add(new Entities.ClassUser { ClassId = classId, email = email });
            }
            var stdEntity = _unitOfWork.Student.Get(x => x.ClassroomId == classId && x.email == email, "");
            if(stdEntity.Count > 0)
            {
                _unitOfWork.Student.Remove(stdEntity.First().Id);
            }
            _unitOfWork.Save();
            return (true, "Successfull");
        }
        

        public IList<Classroom> GetClasses(string mail)
        {
            var classIdEntity = _unitOfWork.ClassUser.Get(x => x.email == mail, "");
            var classRoom = new List<Classroom>();
            foreach(var single in classIdEntity)
            {
                var classEntity = _unitOfWork.Classroom.GetById(single.ClassId);
                var cls = new Classroom
                {
                    ClassroomName = classEntity.ClassroomName,
                    Id = classEntity.Id
                };
                classRoom.Add(cls);
            }
            return classRoom;
        }

        public IList<Teacher> GetTeacherByClassId(int Id)
        {
            var teacherEntities = _unitOfWork.Teacher.Get(x => x.ClassroomId == Id, "");
            var teachers = new List<Teacher>();
            foreach(var teacher in teacherEntities)
            {
                teachers.Add(new Teacher { mail = teacher.email, TeacherId = teacher.Id });
            }
            return teachers;
        }

        public Post Publish(int id)
        {
            var mat = _unitOfWork.Material.GetById(id);
            mat.Status = "Published";
            _unitOfWork.Save();
            var post = _mapper.Map<Post>(mat);
            return post;

        }

        public IList<Student> GetStudentByClassId(int Id)
        {
            var studentsEntities = _unitOfWork.Student.Get(x => x.ClassroomId == Id, "");
            var students = new List<Student>();
            foreach (var student in studentsEntities)
            {
                students.Add(new Student { mail = student.email, StudentId = student.Id });
            }
            return students;
        }

        public (bool isPermited, bool isTeacher, string Name, string Description, int Id) LoadClassRoomData(int classroomId, string mail)
        {
            var count = _unitOfWork.ClassUser.GetCount(x => x.ClassId == classroomId && x.email == mail);
            if(count == 0)
            {
                return (false, false, "", "", -1);
            }
            if(count == 1)
            {
                var teacher = false;
                var classroom = _unitOfWork.Classroom.GetById(classroomId);
                var stdCnt = _unitOfWork.Student.GetCount(x => x.ClassroomId == classroomId && x.email == mail);
                if(stdCnt == 1)
                {
                    teacher = false;
                }
                else
                {
                    teacher = true;
                }
                return (true, teacher, classroom.ClassroomName, classroom.Description, classroom.Id);
            }
            return (false, false, "", "", -1);
        }

        public void CreatePost(Post post)
        {
            if (post == null)
                throw new InvalidOperationException("no Classroom is provided");
            try
            {
                var entity = _mapper.Map<Entities.Post>(post);
                _unitOfWork.Post.Add(entity);
                _unitOfWork.Save();
            }
            catch
            {
                throw new InvalidOperationException("Post not Created");
            }
        }

        public void CreateMaterial(Post post)
        {
            if (post == null)
                throw new InvalidOperationException("no Classroom is provided");
            try
            {
                var entity = _mapper.Map<Entities.Material>(post);
                entity.Status = "Pending";
                _unitOfWork.Material.Add(entity);
                _unitOfWork.Save();
            }
            catch
            {
                throw new InvalidOperationException("Post not Created");
            }
        }

        public IList<Post> GetAllPostByClassId(int classId)
        {
            var posts = new List<Post>();
            var postEntities = _unitOfWork.Post.Get(x => x.ClassroomId == classId, "");
            foreach(var post in postEntities)
            {
                posts.Add(_mapper.Map<Post>(post));
            }
            return posts;
        }

        public IList<Material> GetAllMaterialsByClassId(int classId)
        {
            var materials = new List<Material>();
            var matEntities = _unitOfWork.Material.Get(x => x.ClassroomId == classId, "");
            foreach (var mat in matEntities)
            {
                materials.Add(_mapper.Map<Material>(mat));
            }
            return materials;
        }

    }
}
