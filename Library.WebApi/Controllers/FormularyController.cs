using AutoMapper;
using Library.BLL.Requests.Formulary;
using Library.BLL.Responses.Formulary;
using Library.Common.Models.DTOs.Formulary;
using Library.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[ApiController]
[Route("api/reader")]
public class FormularyController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public FormularyController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFormularyById(Guid id)
    {
        var result = await _mediator.Send(new GetFormularyByIdRequest { Id = id });
        return _mapper.ToActionResult<FormularyResponse, FormularyDTO>(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetFormularies()
    {
        var response = await _mediator.Send(new GetAllFormularyRequest());
        return new OkObjectResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddFormulary([FromBody] CreateFormularyDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateFormularyRequest>(dto));
        return result.Match<IActionResult>(
            book => CreatedAtAction(nameof(GetFormularyById), new { id = book.Id }, book),
            error => BadRequest(error.ToErrorDTO())
        );
    }

    [HttpPut("{id}/take")]
    public async Task<IActionResult> TakeBookByFormulary(Guid id, [FromBody] string[] bookIdsStrings)
    {
        var result = await _mediator.Send(new TakeBookByReaderRequest
            { ReaderId = id, BookIds = bookIdsStrings.Select(bookId => new Guid(bookId)).ToList() });
        
        return result.Match<IActionResult>(
            formulary => CreatedAtAction(nameof(GetFormularyById), new { id = formulary.Id }, formulary),
            error => BadRequest(error.ToErrorDTO())
        );
    }

    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnBookByFormulary(Guid id, [FromBody] string[] bookIdsStrings)
    {
        var result = await _mediator.Send(new ReturnBookByReaderRequest
            { ReaderId = id, BookIds = bookIdsStrings.Select(bookId => new Guid(bookId)).ToList() });
        
        return result.Match<IActionResult>(
            formulary => CreatedAtAction(nameof(GetFormularyById), new { id = formulary.Id }, formulary),
            error => BadRequest(error.ToErrorDTO())
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFormulary(Guid id, [FromBody] UpdateFormularyDTO dto)
    {
        var request = _mapper.Map<UpdateFormularyRequest>(dto);
        request.Id = id;
        var result = await _mediator.Send(request);
        return _mapper.ToActionResult<FormularyResponse, UpdateFormularyDTO>(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFormulary(Guid id)
    {
        var result = await _mediator.Send(new DeleteFormularyRequest { Id = id });
        return result.ToActionResult();
    }
}