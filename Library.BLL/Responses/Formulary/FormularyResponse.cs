using Library.BLL.Responses.Book;

namespace Library.BLL.Responses.Formulary;

public class FormularyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public ICollection<BookResponse> TakenBooks { get; set; } = new HashSet<BookResponse>();


}