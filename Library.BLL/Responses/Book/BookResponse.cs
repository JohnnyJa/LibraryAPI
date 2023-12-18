using Library.Common.Models.DTOs;
using Library.DAL.Entities;

namespace Library.BLL.Services.Responses;

public class BookResponse
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }
    // public int AvailableNumberOfCopies { get; set; }
    public Guid SubjectId { get; set; }
    public SubjectDTO Subject { get; set; }
    public Guid AuthorId { get; set; }
    public AuthorDTO Author { get; set; }
}