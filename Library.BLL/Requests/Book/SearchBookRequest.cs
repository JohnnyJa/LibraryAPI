using Library.BLL.Requests.Base;
using Library.BLL.Responses.Book;
using MediatR;

namespace Library.BLL.Requests.Book;

public class SearchBookRequest : IRequestBase<List<BookResponse>>
{
    public string keyword { get; set; } = string.Empty;
}