using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Formulary;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Formulary;

public class DeleteFormularyRequestHandler : RequestHandlerBase<DeleteFormularyRequest, Deleted>
{
    private readonly IRepository<ReaderFormulary> _repository;
    private readonly IMapper _mapper;

    public DeleteFormularyRequestHandler(IRepository<ReaderFormulary> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<Deleted>> HandleInternal(DeleteFormularyRequest request, CancellationToken cancellationToken)
    {
        var formulary = await _repository.SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (formulary == null)
        {
            return Error.NotFound("Formulary with this id not exist");
        }
        await _repository.DeleteAsync(formulary, cancellationToken);
        return Result.Deleted;
    }
}