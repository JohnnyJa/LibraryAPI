using Library.BLL.Requests.Base;
using Library.BLL.Responses.Subject;

namespace Library.BLL.Requests.Subject;

public class CreateSubjectRequest : IRequestBase<SubjectResponse>
{
    public string Name { get; set; }
}