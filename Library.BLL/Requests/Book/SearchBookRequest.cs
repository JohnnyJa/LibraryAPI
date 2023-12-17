using Library.BLL.Requests.Base;
using Library.BLL.Services.Responses;
using MediatR;

namespace Library.BLL.Requests.Book;

public class SearchBookRequest : IRequestBase<List<BookResponse>>
{
    public List<Guid>? BookIds { get; set; }
    public List<Guid>? AuthorIds { get; set; }
    public List<Guid>? SubjectIds { get; set; }
    public string? Name { get; set; }
    public string? ISBN { get; set; }
}