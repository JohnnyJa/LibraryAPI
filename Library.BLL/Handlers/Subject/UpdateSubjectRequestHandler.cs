using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Subject;
using Library.BLL.Responses.Subject;
using E = Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Subjects;

public class UpdateSubjectRequestHandler : RequestHandlerBase<UpdateSubjectRequest, SubjectResponse>
{
    private readonly IRepository<E.Subject> _repository;
    private readonly IMapper _mapper;

    public UpdateSubjectRequestHandler(IRepository<E.Subject> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<SubjectResponse>> HandleInternal(UpdateSubjectRequest request, CancellationToken cancellationToken)
    {
        var subject = await _repository.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (subject == null)
        {
            return Error.NotFound("Subject with this id does not exist");
        }
        
        _mapper.Map(request, subject);
        await _repository.UpdateAsync(subject, cancellationToken);
        
        return _mapper.Map<SubjectResponse>(subject);
    }
}