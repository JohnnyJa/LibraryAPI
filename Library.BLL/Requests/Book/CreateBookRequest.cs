using Library.BLL.Requests.Base;
using Library.BLL.Services.Responses;
using E = Library.DAL.Entities;
using MediatR;

namespace Library.BLL.Requests.Book;

public class CreateBookRequest : IRequestBase<BookResponse>
{
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }

    public E.Subject Subject { get; set; }
    public E.Author AuthorId { get; set; }
}