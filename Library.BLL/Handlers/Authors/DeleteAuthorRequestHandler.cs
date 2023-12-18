using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Author;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Authors;

public class DeleteAuthorRequestHandler : RequestHandlerBase<DeleteAuthorRequest, Deleted>
{
    private readonly IRepository<Author> _repository;

    public DeleteAuthorRequestHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }

    protected override async Task<ErrorOr<Deleted>> HandleInternal(DeleteAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = await _repository.SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (author == null)
        {
            return Error.NotFound("Author with this id does not exist");
        }
        await _repository.DeleteAsync(author, cancellationToken);
        return Result.Deleted;
    }
}