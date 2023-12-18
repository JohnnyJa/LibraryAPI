using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using ErrorOr;
using Library.BLL.Commands.Base;
using Library.BLL.Requests.Formulary;
using Library.BLL.Responses.Formulary;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Handlers.Formulary;

public class TakeBookByReaderRequestHandler : RequestHandlerBase<TakeBookByReaderRequest, FormularyResponse>
{
    private readonly IRepository<ReaderFormulary> _formulariesRepository;
    private readonly IRepository<Book> _booksRepository;
    private readonly IMapper _mapper;


    public TakeBookByReaderRequestHandler(IRepository<ReaderFormulary> formulariesRepository, IRepository<Book> booksRepository, IMapper mapper)
    {
        _formulariesRepository = formulariesRepository;
        _booksRepository = booksRepository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<FormularyResponse>> HandleInternal(TakeBookByReaderRequest request, CancellationToken cancellationToken)
    {
        var formulary = await _formulariesRepository.Include(f => f.TakenBooks)
            .SingleOrDefaultAsync(f => f.Id == request.ReaderId, cancellationToken);
        
        if (formulary == null)
        {
            return Error.NotFound("Formulary with this id does not exist");
        }
        
        foreach (var bookId in request.BookIds)
        {
            var book = await _booksRepository
                .SingleOrDefaultAsync(f => f.Id == bookId, cancellationToken);
            if (book == null)
            {
                return Error.NotFound("Book with this id does not exist");
            }

            if (!formulary.TakenBooks.Contains(book))
            {
                return Error.NotFound("This book is not taken by this reader");
            }
            formulary.TakenBooks.Add(book);
        }
        
        await _formulariesRepository.UpdateAsync(formulary, cancellationToken);
        return _mapper.Map<FormularyResponse>(formulary);
    }
}