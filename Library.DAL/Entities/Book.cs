using Library.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Entities;

public class Book : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string ISBN { get; set; }
    public Author Author { get; set; }
    public ICollection<ReaderFormulary> ReaderFormularies { get; set; }
}