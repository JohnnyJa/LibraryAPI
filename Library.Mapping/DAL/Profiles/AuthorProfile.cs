using AutoMapper;
using Library.BLL.Requests.Author;
using Library.BLL.Responses.Author;
using Library.Common.Models.DTOs;
using Library.DAL.Entities;

namespace Library.Mapping.DAL.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorResponse>();
        CreateMap<Author, AuthorDTO>();
        CreateMap<CreateAuthorRequest, Author>();
        CreateMap<UpdateAuthorRequest, Author>();
    }
}