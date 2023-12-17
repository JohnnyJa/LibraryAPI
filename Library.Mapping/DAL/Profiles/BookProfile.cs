using AutoMapper;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
using Library.DAL.Entities;

namespace Library.Mapping.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookResponse>();
        CreateMap<BookResponse, Book>();
    }
}