using TaskControl.Entities;
using TaskControl.Models;
using AutoMapper;

namespace TaskControl.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CompanyDTO, Company>().ReverseMap();
        }
    }
}
