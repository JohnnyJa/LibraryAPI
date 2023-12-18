namespace Library.Common.Models.DTOs.Book;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }
    public AuthorDTO Author { get; set; }
    public SubjectDTO Subject { get; set; }
}