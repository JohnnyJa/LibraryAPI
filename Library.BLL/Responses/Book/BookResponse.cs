using Library.BLL.Responses.Author;
using Library.BLL.Responses.Subject;
using Library.Common.Models.DTOs;

namespace Library.BLL.Responses.Book;

public class BookResponse
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }
    public Guid SubjectId { get; set; }
    public SubjectResponse Subject { get; set; }
    public Guid AuthorId { get; set; }
    public AuthorResponse Author { get; set; }
}