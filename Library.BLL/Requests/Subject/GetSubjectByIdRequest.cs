using Library.BLL.Requests.Base;
using Library.BLL.Responses.Author;
using Library.BLL.Responses.Subject;

namespace Library.BLL.Requests.Author;

public class GetSubjectByIdRequest : IRequestBase<SubjectResponse>
{
    public Guid Id { get; set; }
}