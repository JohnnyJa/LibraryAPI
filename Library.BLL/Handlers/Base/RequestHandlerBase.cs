using ErrorOr;
using Library.BLL.Requests.Base;
using MediatR;

namespace Library.BLL.Commands.Base;

public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<TResponse>> where TRequest : IRequestBase<TResponse>
{
    public async Task<ErrorOr<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return await HandleInternal(request, cancellationToken);
    }

    protected abstract Task<ErrorOr<TResponse>> HandleInternal(TRequest request, CancellationToken cancellationToken);
}