using Autofac;
using ClassroomV2.Manager.Context;
using ClassroomV2.Manager.Repositories;
using ClassroomV2.Manager.Services;
using ClassroomV2.Manager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager
{
    public class ManagerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ManagerModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManagerContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ManagerContext>().As<IManagerContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ManagerUnitOfWork>().As<IManagerUnitOfWorks>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClassroomRepository>().As<IClassroomRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClassUserRepository>().As<IClassUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TeacherRepository>().As<ITeacherRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PostRepository>().As<IPostRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MaterialRepository>().As<IMaterialRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClassroomService>().As<IClassroomService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
