using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Subject;
using Library.BLL.Responses.Subject;
using Library.DAL.Repository.IRepository;
using E = Library.DAL.Entities;

namespace Library.BLL.Handlers.Subject;

public class CreateSubjectRequestHandler : RequestHandlerBase<CreateSubjectRequest, SubjectResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<E.Subject> _repository;

    public CreateSubjectRequestHandler(IMapper mapper, IRepository<E.Subject> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    protected override async Task<ErrorOr<SubjectResponse>> HandleInternal(CreateSubjectRequest request, CancellationToken cancellationToken)
    {
        var subject = _mapper.Map<E.Subject>(request);
        await _repository.AddAsync(subject, cancellationToken);
        return _mapper.Map<SubjectResponse>(subject);
    }
}