using AutoMapper;
using Library.BLL.Requests.Subject;
using Library.BLL.Responses.Subject;
using Library.DAL.Entities;

namespace Library.Mapping.DAL.Profiles;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectResponse>();
        CreateMap<CreateSubjectRequest, Subject>();
        CreateMap<UpdateSubjectRequest, Subject>();
    }
}