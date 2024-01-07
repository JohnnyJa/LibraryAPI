using ErrorOr;
using Library.BLL.Requests.Base;

namespace Library.BLL.Requests.Book;

public class DeleteBookRequest :IRequestBase<Deleted>   
{
    public Guid Id { get; set; }
}