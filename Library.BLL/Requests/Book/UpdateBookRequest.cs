using Library.BLL.Requests.Base;
using Library.BLL.Services.Responses;
using E = Library.DAL.Entities;

namespace Library.BLL.Requests.Book;

public class UpdateBookRequest :IRequestBase<BookResponse>
{
    public Guid BookId { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
    
    public E.Subject Subject { get; set; }
    
    public int NumberOfCopies { get; set; }
    public E.Author Author { get; set; }
    public ICollection<E.ReaderFormulary> ReaderFormularies { get; set; }
}