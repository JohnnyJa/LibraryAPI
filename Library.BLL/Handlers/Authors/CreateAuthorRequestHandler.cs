using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Author;
using Library.BLL.Requests.Book;
using Library.BLL.Responses.Author;
using Library.BLL.Services.Responses;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;

namespace Library.BLL.Handlers.Authors;

public class CreateAuthorRequestHandler : RequestHandlerBase<CreateAuthorRequest, AuthorResponse>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public CreateAuthorRequestHandler(IMapper mapper, IRepository<Author> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    protected override async Task<ErrorOr<AuthorResponse>> HandleInternal(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Author>(request);
        await _repository.AddAsync(book, cancellationToken);
        return  _mapper.Map<AuthorResponse>(book);
    }
}