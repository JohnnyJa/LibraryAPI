using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Author;
using Library.BLL.Requests.Subject;
using Library.BLL.Responses.Subject;
using E = Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Subjects;

public class GetSubjectByIdRequestHandler : RequestHandlerBase<GetSubjectByIdRequest, SubjectResponse>
{
    
    private readonly IRepository<E.Subject> _repository;
    private readonly IMapper _mapper;

    public GetSubjectByIdRequestHandler(IRepository<E.Subject> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<SubjectResponse>> HandleInternal(GetSubjectByIdRequest request, CancellationToken cancellationToken)
    {
        var subject = await _repository
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        
        if (subject == null)
        {
            return Error.NotFound("Subject with this id does not exist");
        }

        return _mapper.Map<SubjectResponse>(subject);
    }
}