using AutoMapper;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
using Library.DAL.Entities;

namespace Library.Common.WebAPI.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookRequest, BookResponse>();
        CreateMap<BookResponse, CreateBookRequest>();
    }
}