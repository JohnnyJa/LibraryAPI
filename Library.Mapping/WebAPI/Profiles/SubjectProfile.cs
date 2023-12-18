using AutoMapper;
using Library.BLL.Requests.Subject;
using Library.BLL.Responses.Subject;
using Library.Common.Models.DTOs;

namespace Library.Mapping.WebAPI.Profiles;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<SubjectResponse, SubjectDTO>();
        CreateMap<CreateSubjectDTO, CreateSubjectRequest>();
        CreateMap<UpdateSubjectDTO, UpdateSubjectRequest>();
    }
}