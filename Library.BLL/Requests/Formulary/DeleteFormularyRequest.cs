using ErrorOr;
using Library.BLL.Requests.Base;

namespace Library.BLL.Requests.Formulary;

public class DeleteFormularyRequest : IRequestBase<Deleted>
{
    public Guid Id { get; set; }
}