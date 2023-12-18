using AutoMapper;
using Library.BLL.Requests.Author;
using Library.BLL.Requests.Subject;
using Library.BLL.Responses.Subject;
using Library.Common.Models.DTOs;
using Library.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public SubjectController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(Guid id)
    {
        var result = await _mediator.Send(new GetSubjectByIdRequest { Id = id });
        return _mapper.ToActionResult<SubjectResponse, SubjectDTO>(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateSubjectRequest>(dto));
        return result.Match<IActionResult>(
            author => CreatedAtAction(nameof(GetSubjectById), new { id = author.Id }, author),
            error => BadRequest(error.ToErrorDTO())
        );
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] UpdateSubjectDTO dto)
    {
        var request = _mapper.Map<UpdateSubjectRequest>(dto);
        request.Id = id;
        var result = await _mediator.Send(request);
        return _mapper.ToActionResult<SubjectResponse, SubjectDTO>(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        var result = await _mediator.Send(new DeleteSubjectRequest { Id = id });
        return result.ToActionResult();
    }
}
