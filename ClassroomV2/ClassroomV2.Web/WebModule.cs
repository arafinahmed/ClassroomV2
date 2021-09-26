using Autofac;
using ClassroomV2.Web.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomV2.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateClassroomModel>().AsSelf();
            builder.RegisterType<LoadClassroom>().AsSelf();            

            base.Load(builder);
        }
    }
}
