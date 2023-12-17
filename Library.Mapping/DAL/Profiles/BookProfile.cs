using AutoMapper;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
using Library.DAL.Entities;

namespace Library.Mapping.DAL.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookResponse>();
        CreateMap<CreateBookRequest, Book>();
    }
}