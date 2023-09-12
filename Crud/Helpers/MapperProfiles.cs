using AutoMapper;
using Crud.DTOs.ContactsDTOs;
using Crud.DTOs.EducationsDTOs;
using Crud.DTOs.ExperiencesDTOs;
using Crud.DTOs.InformationDTOs;
using Crud.DTOs.SkillsDTOs;
using CrudApp.Models;

namespace Crud.Helpers
{
    public class MapperProfiles: Profile
    {
 
        public MapperProfiles() {

            //Information
            CreateMap<Information, InformationReadDTOs>();
            CreateMap<InformationCreateDTOs, Information>();
            CreateMap<InformationUpdateDTOs, Information>();
            CreateMap<Information, InformationUpdateDTOs>();
            
            //Skills
            CreateMap<Skills, SkillReadDTOs>();
            CreateMap<SkillCreateDTOs,Skills >();
            CreateMap<Skills, SkillsUpdateDTOs>();
            CreateMap<SkillsUpdateDTOs, Skills>();
            
            //Contact
            CreateMap<Contact, ContactReadDTOs>();
            CreateMap<Contact,ContactUpdateDTOs>();
            CreateMap<ContactUpdateDTOs, Contact>();
            CreateMap<ContactCreateDTOs,Contact>();



            //Experiences
            CreateMap<Experience, ExperiencesReadDTOs>();
            CreateMap<ExperiencesCreateDTOs,Experience>();
            CreateMap<Experience, ExperiencesUpdateDTOs>();
            CreateMap<ExperiencesUpdateDTOs,Experience >();


            //Educations
            CreateMap<Education, EducationsReadDTOs>();
            CreateMap<EducationsCreateDTOs, Education>();
            CreateMap<Education, EducationsUpdateDTOs>();
            CreateMap<EducationsUpdateDTOs, Education>();



        }
    }
}
