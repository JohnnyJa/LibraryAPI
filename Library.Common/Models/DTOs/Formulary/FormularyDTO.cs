using Library.Common.Models.DTOs.Book;

namespace Library.Common.Models.DTOs.Formulary;

public class FormularyDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<BookDTO> TakenBooks { get; set; }
}