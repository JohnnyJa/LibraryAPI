using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Books;

public class SearchBookRequestHandler : RequestHandlerBase<SearchBookRequest, List<BookResponse>>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public SearchBookRequestHandler(IMapper mapper, IRepository<Book> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    protected override async Task<ErrorOr<List<BookResponse>>> HandleInternal(SearchBookRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Book> queryable = _repository.Include(b => b.Author)
            .Include(b => b.Subject)
            .Include(b => b.ReaderFormularies);
        if (request.BookIds != null)
            queryable = queryable.Where(b => request.BookIds.Contains(b.Id));
        
        if (request.AuthorIds != null)
            queryable = queryable.Where(b => request.AuthorIds.Contains(b.Author.Id));
        
        if (request.SubjectIds != null)
            queryable = queryable.Where(b => request.SubjectIds.Contains(b.Subject.Id));
        
        if (request.Name != null)
            queryable = queryable.Where(b => b.Name == request.Name);
        
        if (request.ISBN != null)
            queryable = queryable.Where(b => b.ISBN == request.ISBN);
        
        var books = await queryable.ToListAsync(cancellationToken);
        
        return _mapper.Map<List<BookResponse>>(books);
    }
}