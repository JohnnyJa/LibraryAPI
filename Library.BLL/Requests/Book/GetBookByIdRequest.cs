using Library.BLL.Requests.Base;
using Library.BLL.Responses.Book;

namespace Library.BLL.Requests.Book;

public class GetBookByIdRequest :IRequestBase<BookResponse>
{
    public Guid Id { get; set; }
}