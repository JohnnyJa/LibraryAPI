using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Book;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Commands.Books;

public class DeleteBookRequestHandler :RequestHandlerBase<DeleteBookRequest, Deleted>
{
    private readonly IRepository<Book> _repository;

    public DeleteBookRequestHandler(IRepository<Book> repository)
    {
        _repository = repository;
    }

    protected override async Task<ErrorOr<Deleted>> HandleInternal(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        var book = await _repository.FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);
        if (book == null)
        {
            return Error.NotFound("Book with this id not exist");
        }

        await _repository.DeleteAsync(book, cancellationToken);
        return Result.Deleted;
    }
}