namespace Library.BLL.Responses.Formulary;

public class FormularyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public ICollection<Guid> TakenBooksId { get; set; } = new HashSet<Guid>();

}