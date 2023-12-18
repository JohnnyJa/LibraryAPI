using AutoMapper;
using Library.BLL.Requests.Author;
using Library.BLL.Responses.Author;
using Library.Common.Models.DTOs;
using Library.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public AuthorController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var result = await _mediator.Send(new GetAuthorByIdRequest { Id = id });
        return _mapper.ToActionResult<AuthorResponse, AuthorDTO>(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorRequest dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateAuthorRequest>(dto));
        return result.Match<IActionResult>(
            author => CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author),
            error => BadRequest(error.ToErrorDTO())
        );
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorRequest dto)
    {
        var request = _mapper.Map<UpdateAuthorRequest>(dto);
        request.Id = id;
        var result = await _mediator.Send(request);
        return _mapper.ToActionResult<AuthorResponse, AuthorDTO>(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var result = await _mediator.Send(new DeleteAuthorRequest { Id = id });
        return result.ToActionResult();
    }
}
