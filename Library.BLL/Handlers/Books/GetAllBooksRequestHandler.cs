using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
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
        var quaryable = _repository.Include(b=>b.Author)
            .Include(b=>b.Subject)
            .Include(b =>b.ReaderFormularies);
        var books  = await quaryable.ToListAsync(cancellationToken);
        return _mapper.Map<List<BookResponse>>(books);
    }
}