using AutoMapper;
using Library.BLL.Requests.Book;
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

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _mediator.Send(new GetAllBooksRequest());
        return new OkObjectResult(response);
    }
}