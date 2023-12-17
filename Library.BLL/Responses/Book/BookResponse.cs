using Library.DAL.Entities;

namespace Library.BLL.Services.Responses;

public class BookResponse
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int TotalNumberOfCopies { get; set; }
    public int AvailableNumberOfCopies { get; set; }
    
    public Subject Subject { get; set; }
    public Author Author { get; set; }
}