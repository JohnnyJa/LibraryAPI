using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Library.DAL.Entities.Base;

namespace Library.DAL.Entities;

public class ReaderFormulary : BaseEntity<Guid>
{
    
    public string Name { get; set; } = null!;
     
    public string Surname { get; set; } = null!;

    public ICollection<Book> TakenBooks { get; set; } = new HashSet<Book>();
}