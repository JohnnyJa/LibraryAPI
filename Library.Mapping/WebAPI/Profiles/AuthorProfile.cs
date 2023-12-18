using AutoMapper;
using Library.BLL.Requests.Author;
using Library.Common.Models.DTOs;

namespace Library.Mapping.WebAPI.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<CreateAuthorDTO, CreateAuthorRequest>();
        // CreateMap<AuthorResponse, CreateAuthorRequest>();
    }
}