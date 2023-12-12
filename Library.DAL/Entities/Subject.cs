using Library.DAL.Entities.Base;

namespace Library.DAL.Entities;

public class Subject : BaseEntity<Guid>
{
    public string Name { get; set; }
}