using Library.BLL.Requests.Base;
using Library.BLL.Responses.Formulary;

namespace Library.BLL.Requests.Formulary;

public class UpdateFormularyRequest : IRequestBase<FormularyResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}