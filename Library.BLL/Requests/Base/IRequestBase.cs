using ErrorOr;
using MediatR;

namespace Library.BLL.Requests.Base;

public interface IRequestBase<TResponse> : IRequest<ErrorOr<TResponse>> { }