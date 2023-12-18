using AutoMapper;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
using Library.Common.Models.DTOs.Book;
using Library.DAL.Entities;

namespace Library.Common.WebAPI.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookResponse,BookDTO>();
        CreateMap<BookResponse,UpdateBookDTO>();
        CreateMap<CreateBookDTO, CreateBookRequest>();
        CreateMap<UpdateBookDTO, UpdateBookRequest>();
    
    }
}