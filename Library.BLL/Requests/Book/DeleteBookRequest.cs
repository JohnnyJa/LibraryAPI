using ErrorOr;
using Library.BLL.Requests.Base;

namespace Library.BLL.Requests.Book;

public class DeleteBookRequest :IRequestBase<Deleted>   
{
    public Guid BookId { get; set; }
}