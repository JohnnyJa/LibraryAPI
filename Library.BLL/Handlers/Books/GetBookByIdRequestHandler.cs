using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Responses.Book;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Commands.Books;

public class GetBookByIdRequestHandler : RequestHandlerBase<GetBookByIdRequest, BookResponse>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public GetBookByIdRequestHandler( IRepository<Book> repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    protected override async Task<ErrorOr<BookResponse>> HandleInternal(GetBookByIdRequest request, CancellationToken cancellationToken)
    {
        var book = await _repository
            .Include(b => b.Author)
            .Include(b => b.Subject)
            .Include(b => b.ReaderFormularies)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        
        if (book == null)
        {
            return Error.NotFound("Book with this id does not exist");
        }

        return _mapper.Map<BookResponse>(book);
    }
}