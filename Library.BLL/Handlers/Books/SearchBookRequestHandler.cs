using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Responses.Book;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Books;

public class SearchBookRequestHandler : RequestHandlerBase<SearchBookRequest, List<BookResponse>>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public SearchBookRequestHandler( IRepository<Book> repository,IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    protected override async Task<ErrorOr<List<BookResponse>>> HandleInternal(SearchBookRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Book> queryable = _repository.Include(b => b.Author)
            .Include(b => b.Subject)
            .Include(b => b.ReaderFormularies)
            .Where(b => b.Name.Contains(request.keyword)
                        || b.ISBN.Contains(request.keyword)
                        || b.Author.Name.Contains(request.keyword)
                        || b.Author.Surname.Contains(request.keyword)
                        || b.Subject.Name.Contains(request.keyword));
            
            
        
        var books = await queryable.ToListAsync(cancellationToken);
        
        return _mapper.Map<List<BookResponse>>(books);
    }
}