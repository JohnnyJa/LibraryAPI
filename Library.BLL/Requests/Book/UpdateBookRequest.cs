using Library.BLL.Requests.Base;
using Library.BLL.Responses.Book;
using E = Library.DAL.Entities;

namespace Library.BLL.Requests.Book;

public class UpdateBookRequest :IRequestBase<BookResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }
    
    public Guid SubjectId { get; set; }
    
    public Guid AuthorId { get; set; }
}