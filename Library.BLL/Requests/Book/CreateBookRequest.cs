using Library.BLL.Requests.Base;
using Library.BLL.Responses.Book;
using E = Library.DAL.Entities;
using MediatR;

namespace Library.BLL.Requests.Book;

public class CreateBookRequest : IRequestBase<BookResponse>
{
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }

    public Guid SubjectId { get; set; }
    public Guid AuthorId { get; set; }
}