using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Formulary;
using Library.BLL.Responses.Formulary;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Formulary;

public class GetAllFormularyRequestHandler : RequestHandlerBase<GetAllFormularyRequest, List<FormularyResponse>>
{
    private readonly IRepository<ReaderFormulary> _repository;
    private readonly IMapper _mapper;

    public GetAllFormularyRequestHandler(IRepository<ReaderFormulary> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<List<FormularyResponse>>> HandleInternal(GetAllFormularyRequest request, CancellationToken cancellationToken)
    {
        var formulary = await _repository.Include(f => f.TakenBooks)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<FormularyResponse>>(formulary);
    }
}