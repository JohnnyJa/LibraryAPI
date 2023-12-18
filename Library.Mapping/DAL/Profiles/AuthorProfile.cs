using AutoMapper;
using Library.BLL.Requests.Author;
using Library.BLL.Responses.Author;
using Library.DAL.Entities;

namespace Library.Mapping.DAL.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorResponse>();
        CreateMap<CreateAuthorRequest, Author>();
        CreateMap<UpdateAuthorRequest, Author>();
    }
}