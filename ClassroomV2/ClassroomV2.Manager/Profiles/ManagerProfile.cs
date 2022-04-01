using AutoMapper;
using EO = ClassroomV2.Manager.Entities;
using BO = ClassroomV2.Manager.BusinessObjects;

namespace ClassroomV2.Manager.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<EO.Classroom, BO.Classroom>().ReverseMap();
            CreateMap<EO.Post, BO.Post>().ReverseMap();
            CreateMap<EO.Material, BO.Post>().ReverseMap();
            CreateMap<EO.Material, BO.Material>().ReverseMap();
        }
    }
}
