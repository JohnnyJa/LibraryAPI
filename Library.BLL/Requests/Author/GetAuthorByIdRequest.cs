using Library.BLL.Requests.Base;
using Library.BLL.Responses.Author;

namespace Library.BLL.Requests.Author;

public class GetAuthorByIdRequest : IRequestBase<AuthorResponse>
{
    public Guid Id { get; set; }
}