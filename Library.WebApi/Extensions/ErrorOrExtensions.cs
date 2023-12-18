using AutoMapper;
using ErrorOr;
using Library.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Extensions;

public static class ErrorOrExtensions
{
    public static ErrorDTO ToErrorDTO(this IEnumerable<Error> errors)
    {
        return new ErrorDTO
        {
            Errors = errors.Select(e => e.Code)
        };
    }
    
    public static IActionResult ToActionResult<TSrc, TDest>(this IMapper mapper, ErrorOr<TSrc> errorOr)
    {
        return errorOr.Match<IActionResult>(
            value => new OkObjectResult(mapper.Map<TDest>(value)),
            errors => new BadRequestObjectResult(errors.ToErrorDTO()));
    }
    
    public static IActionResult ToActionResult(this ErrorOr<Deleted> errorOr)
    {
        return errorOr.Match<IActionResult>(
            _ => new NoContentResult(),
            errors => new BadRequestObjectResult(errors.ToErrorDTO()));
    }
    
    public static IActionResult ToActionResult(this ErrorOr<Success> errorOr)
    {
        return errorOr.Match<IActionResult>(
            _ => new NoContentResult(),
            errors => new BadRequestObjectResult(errors.ToErrorDTO()));
    }
}