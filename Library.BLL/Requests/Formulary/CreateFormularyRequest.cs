using Library.BLL.Requests.Base;
using Library.BLL.Responses.Formulary;

namespace Library.BLL.Requests.Formulary;

public class CreateFormularyRequest : IRequestBase<FormularyResponse>
{
    public string Name { get; set; }
    public string Surname { get; set; }
}