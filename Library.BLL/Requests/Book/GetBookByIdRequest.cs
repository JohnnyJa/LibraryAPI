using Library.BLL.Requests.Base;
using Library.BLL.Services.Responses;

namespace Library.BLL.Requests.Book;

public class GetBookByIdRequest :IRequestBase<BookResponse>
{
    public Guid BookId { get; set; }
}