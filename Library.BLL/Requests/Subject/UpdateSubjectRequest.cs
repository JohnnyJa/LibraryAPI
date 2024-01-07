using Library.BLL.Requests.Base;
using Library.BLL.Responses.Subject;

namespace Library.BLL.Requests.Subject;

public class UpdateSubjectRequest : IRequestBase<SubjectResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}