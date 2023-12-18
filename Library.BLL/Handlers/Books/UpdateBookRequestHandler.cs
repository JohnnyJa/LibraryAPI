using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Book;
using Library.BLL.Services.Responses;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Commands.Books;

public class UpdateBookRequestHandler : RequestHandlerBase<UpdateBookRequest, BookResponse>
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public UpdateBookRequestHandler(IRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<BookResponse>> HandleInternal(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var book = await _repository.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (book == null)
        {
            return Error.NotFound("Book with this id does not exist");
        }

        _mapper.Map(request, book);
        await _repository.UpdateAsync(book, cancellationToken);
        return _mapper.Map<BookResponse>(book);
    }
}