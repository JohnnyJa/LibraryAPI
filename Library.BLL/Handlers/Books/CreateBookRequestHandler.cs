using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
using Library.DAL.Entities;
using Library.DAL.Repository;
using Library.DAL.Repository.IRepository;
using MediatR;
using MediatR.Wrappers;

namespace Library.BLL.Commands.Books;

public class CreateBookRequestHandler : RequestHandlerBase<CreateBookRequest, BookResponse>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public CreateBookRequestHandler(IRepository<Book> repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    protected override async Task<ErrorOr<BookResponse>> HandleInternal(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(request);
        await _repository.AddAsync(book, cancellationToken);
        return  _mapper.Map<BookResponse>(book);
    }
}