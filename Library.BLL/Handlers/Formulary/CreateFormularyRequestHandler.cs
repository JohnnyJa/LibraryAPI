using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Base;
using Library.BLL.Requests.Formulary;
using Library.BLL.Responses.Formulary;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;

namespace Library.BLL.Handlers.Formulary;

public class CreateFormularyRequestHandler : RequestHandlerBase<CreateFormularyRequest, FormularyResponse>
{
    private readonly IRepository<ReaderFormulary> _repository;
    private readonly IMapper _mapper;


    public CreateFormularyRequestHandler(IRepository<ReaderFormulary> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<FormularyResponse>>  HandleInternal(CreateFormularyRequest request, CancellationToken cancellationToken)
    {
        var formulary = _mapper.Map<ReaderFormulary>(request);
        await _repository.AddAsync(formulary, cancellationToken);
        return _mapper.Map<FormularyResponse>(formulary);
    }
}