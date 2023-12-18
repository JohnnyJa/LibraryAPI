using Library.BLL.Requests.Base;
using Library.BLL.Responses.Author;

namespace Library.BLL.Requests.Author;

public class UpdateAuthorRequest : IRequestBase<AuthorResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}