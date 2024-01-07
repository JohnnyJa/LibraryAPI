using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Formulary;
using Library.BLL.Responses.Formulary;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Formulary;

public class GetFormularyByIdRequestHandler : RequestHandlerBase<GetFormularyByIdRequest, FormularyResponse>
{
    private readonly IRepository<ReaderFormulary> _repository;
    private readonly IMapper _mapper;

    public GetFormularyByIdRequestHandler(IRepository<ReaderFormulary> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<FormularyResponse>> HandleInternal(GetFormularyByIdRequest request, CancellationToken cancellationToken)
    {
        var formulary = await _repository.Include(f => f.TakenBooks)
            .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        
        if (formulary == null)
        {
            return Error.NotFound("Formulary with this id does not exist");
        }
        
        return _mapper.Map<FormularyResponse>(formulary);
    }
}