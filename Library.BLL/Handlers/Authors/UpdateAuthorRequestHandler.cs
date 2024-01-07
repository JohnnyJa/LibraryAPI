using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Author;
using Library.BLL.Responses.Author;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Authors;

public class UpdateAuthorRequestHandler : RequestHandlerBase<UpdateAuthorRequest, AuthorResponse>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public UpdateAuthorRequestHandler(IRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<AuthorResponse>> HandleInternal(UpdateAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = await _repository.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (author == null)
        {
            return Error.NotFound("Author with this id does not exist");
        }
        
        _mapper.Map(request, author);
        await _repository.UpdateAsync(author, cancellationToken);
        
        return _mapper.Map<AuthorResponse>(author);
    }
}