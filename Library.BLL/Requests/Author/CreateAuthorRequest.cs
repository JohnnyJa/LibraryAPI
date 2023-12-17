using Library.BLL.Requests.Base;
using Library.BLL.Responses.Author;

namespace Library.BLL.Requests.Author;

public class CreateAuthorRequest : IRequestBase<AuthorResponse>
{
    public string Name { get; set; }
    public string Surname { get; set; }
}