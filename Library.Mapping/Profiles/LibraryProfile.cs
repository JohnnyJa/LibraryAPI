using AutoMapper;
using Library.BLL.Requests.Author;
using Library.BLL.Requests.Book;
using Library.BLL.Requests.Formulary;
using Library.BLL.Requests.Subject;
using Library.BLL.Responses.Author;
using Library.BLL.Responses.Book;
using Library.BLL.Responses.Formulary;
using Library.BLL.Responses.Subject;
using Library.Common.Models.DTOs;
using Library.Common.Models.DTOs.Book;
using Library.Common.Models.DTOs.Formulary;
using Library.DAL.Entities;

namespace Library.Mapping.DAL.Profiles;

public class LibraryProfile : Profile
{
    public LibraryProfile()
    {
        CreateMap<Subject, SubjectResponse>();
        CreateMap<Subject, SubjectDTO>();
        CreateMap<SubjectResponse, SubjectDTO>();

        CreateMap<CreateSubjectRequest, Subject>();
        CreateMap<UpdateSubjectRequest, Subject>();
        
        CreateMap<CreateSubjectDTO, CreateSubjectRequest>();
        CreateMap<UpdateSubjectDTO, UpdateSubjectRequest>();
        
        CreateMap<ReaderFormulary, FormularyResponse>();
        CreateMap<CreateFormularyRequest, ReaderFormulary>();
        CreateMap<UpdateFormularyRequest, ReaderFormulary>();
        
        CreateMap<CreateFormularyDTO, CreateFormularyRequest>();
        CreateMap<UpdateFormularyDTO, UpdateFormularyRequest>();
        CreateMap<FormularyResponse, FormularyDTO>();
        CreateMap<FormularyResponse, UpdateFormularyDTO>();
        
        CreateMap<Book, BookResponse>();
        CreateMap<CreateBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
        
        CreateMap<BookResponse,BookDTO>();
        CreateMap<BookResponse,UpdateBookDTO>();
        CreateMap<CreateBookDTO, CreateBookRequest>();
        CreateMap<UpdateBookDTO, UpdateBookRequest>();
        
        CreateMap<Author, AuthorResponse>();
        CreateMap<Author, AuthorDTO>();
        CreateMap<CreateAuthorRequest, Author>();
        CreateMap<UpdateAuthorRequest, Author>();
        
        CreateMap<AuthorResponse, AuthorDTO>();
        CreateMap<CreateAuthorDTO, CreateAuthorRequest>();
        CreateMap<UpdateAuthorDTO, UpdateAuthorRequest>();
    }
}