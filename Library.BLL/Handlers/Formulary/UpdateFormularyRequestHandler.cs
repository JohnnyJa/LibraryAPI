using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Formulary;
using Library.BLL.Responses.Formulary;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Formulary;

public class UpdateFormularyRequestHandler : RequestHandlerBase<UpdateFormularyRequest, FormularyResponse>
{
    private readonly IRepository<ReaderFormulary> _repository;
    private readonly IMapper _mapper;

    public UpdateFormularyRequestHandler(IRepository<ReaderFormulary> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<FormularyResponse>> HandleInternal(UpdateFormularyRequest request, CancellationToken cancellationToken)
    {
        var formulary = _repository.Include(f => f.TakenBooks)
            .SingleOrDefault(f => f.Id == request.Id);
        if (formulary == null)
        {
            return Error.NotFound("Formulary with this id does not exist");
        }
        
        _mapper.Map(request, formulary);
        await _repository.UpdateAsync(formulary, cancellationToken);
        return _mapper.Map<FormularyResponse>(formulary);
    }
}