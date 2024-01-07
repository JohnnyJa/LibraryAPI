using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Responses.Book;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Commands.Books;

public class GetAllBooksRequestHandler : RequestHandlerBase<GetAllBooksRequest, List<BookResponse>>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public GetAllBooksRequestHandler(IRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<List<BookResponse>>> HandleInternal(GetAllBooksRequest request, CancellationToken cancellationToken)
    {
        var books =  await _repository.Include("Author")
            .Include("Subject")
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<BookResponse>>(books);
    }
}