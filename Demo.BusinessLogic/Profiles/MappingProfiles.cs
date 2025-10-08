using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;
using Demo.DataAccess.Models.EmployeeModel;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDTO>()//    (Dest,Options(Source))
                .ForMember(dest => dest.EmpGender, Options => Options.MapFrom(Src => Src.Gender))
                .ForMember(dest => dest.EmpType, Options => Options.MapFrom(Src => Src.EmployeeType))
                .ForMember(dest => dest.Department, Options => Options.MapFrom
                                            (Src => Src.Department!= null? Src.Department.Name:null));
            CreateMap<Employee, EmployeeDetailsDTO>()
                .ForMember(dest => dest.Gender, Options => Options.MapFrom(Src => Src.Gender))
                .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(Src => Src.EmployeeType))
                .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(Src => DateOnly.FromDateTime(Src.HiringDate)))
                .ForMember(dest => dest.Department, Options => Options.MapFrom
                                            (Src => Src.Department != null ? Src.Department.Name : null));
            CreateMap<CreatedEmployeeDTO, Employee>()
                .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(Src => Src.HiringDate.ToDateTime(TimeOnly.MinValue)));
            CreateMap<UpdatedEmployeeDTO, Employee>()
                .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(Src => Src.HiringDate.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
