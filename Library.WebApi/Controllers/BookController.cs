using AutoMapper;
using Library.BLL.Requests.Book;
using Library.BLL.Responses.Book;
using Library.Common.Models.DTOs.Book;
using Library.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BookController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var result = await _mediator.Send(new GetBookByIdRequest { Id = id });
        return _mapper.ToActionResult<BookResponse, BookDTO>(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery]string? keyword)
    {
        if (keyword is null)
        {
            var response = await _mediator.Send(new GetAllBooksRequest());
            return new OkObjectResult(response);
        }
        else
        {
            var result = await _mediator.Send(new SearchBookRequest() { keyword = keyword });
            return new OkObjectResult(result);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateBookRequest>(dto));
        return result.Match<IActionResult>(
            book => CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book),
            error => BadRequest(error.ToErrorDTO())
        );
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDTO dto)
    {
        var request = _mapper.Map<UpdateBookRequest>(dto);
        request.Id = id;
        var result = await _mediator.Send(request);
        return _mapper.ToActionResult<BookResponse, UpdateBookDTO>(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var result = await _mediator.Send(new DeleteBookRequest { Id = id });
        return result.ToActionResult();
    }
    
    
}