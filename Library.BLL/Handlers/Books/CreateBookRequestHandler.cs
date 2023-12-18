using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Responses.Book;
using Library.DAL.Entities;
using Library.DAL.Repository;
using Library.DAL.Repository.IRepository;
using MediatR;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Commands.Books;

public class CreateBookRequestHandler : RequestHandlerBase<CreateBookRequest, BookResponse>
{
    private readonly IRepository<Book> _repository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<Subject> _subjectRepository;

    private readonly IMapper _mapper;

    public CreateBookRequestHandler(IRepository<Book> repository, IRepository<Author> authorsRepository, IRepository<Subject> subjectRepository, IMapper mapper)
    {
        _mapper = mapper;
        _subjectRepository = subjectRepository;
        _authorRepository = authorsRepository;
        _repository = repository;
    }

    protected override async Task<ErrorOr<BookResponse>> HandleInternal(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.SingleOrDefaultAsync(a => a.Id == request.AuthorId, cancellationToken);
        if (author == null)
        {
            return Error.NotFound("Author does not exist");
        }
        
        var subject = await _subjectRepository.SingleOrDefaultAsync(s => s.Id == request.SubjectId, cancellationToken);
        if (subject == null)
        {
            return Error.NotFound("Subject does not exist");
        }
        
        var book = _mapper.Map<Book>(request);
        await _repository.AddAsync(book, cancellationToken);
        return  _mapper.Map<BookResponse>(book);
    }
}