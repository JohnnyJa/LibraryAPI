using ErrorOr;
using Library.BLL.Requests.Base;

namespace Library.BLL.Requests.Author;

public class DeleteSubjectRequest: IRequestBase<Deleted> 
{
    public Guid Id { get; set; }
}