namespace Library.Common.Models.DTOs.Book;

public class CreateBookDTO
{
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }
    public Guid AuthorId { get; set; }
    public Guid SubjectId { get; set; }
}