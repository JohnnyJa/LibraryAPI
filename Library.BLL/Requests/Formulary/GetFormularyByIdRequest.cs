using Library.BLL.Requests.Base;
using Library.BLL.Responses.Formulary;

namespace Library.BLL.Requests.Formulary;

public class GetFormularyByIdRequest : IRequestBase<FormularyResponse>
{
    public Guid Id { get; set; }
}