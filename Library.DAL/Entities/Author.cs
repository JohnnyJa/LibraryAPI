using Library.DAL.Entities.Base;

namespace Library.DAL.Entities;

public class Author : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Book> Books { get; set; } = new HashSet<Book>();
}