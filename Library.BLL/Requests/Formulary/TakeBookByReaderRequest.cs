using Library.BLL.Requests.Base;
using Library.BLL.Responses.Formulary;

namespace Library.BLL.Requests.Formulary;

public class TakeBookByReaderRequest : IRequestBase<FormularyResponse>
{
    public Guid ReaderId { get; set; }
    public ICollection<Guid> BookIds { get; set; } = new List<Guid>();
}