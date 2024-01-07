using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Author;
using Library.BLL.Requests.Subject;
using E =Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Subjects;

public class DeleteSubjectRequestHandler : RequestHandlerBase<DeleteSubjectRequest, Deleted>
{
    private readonly IRepository<E.Subject> _repository;

    public DeleteSubjectRequestHandler(IRepository<E.Subject> repository)
    {
        _repository = repository;
    }

    protected override async Task<ErrorOr<Deleted>> HandleInternal(DeleteSubjectRequest request, CancellationToken cancellationToken)
    {
        var subject = await _repository.SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (subject == null)
        {
            return Error.NotFound("Subject with this id does not exist");
        }
        await _repository.DeleteAsync(subject, cancellationToken);
        return Result.Deleted;
    }
}