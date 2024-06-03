using Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Abstractions.Bus;
public class InMemoryBus : IMediatorHandler
{
    private readonly IMediator _mediator;
    private readonly ILogger<InMemoryBus> _logger;

    //
    // Summary:
    //     Construtor
    //
    // Parameters:
    //   mediator:
    //     Instância do MediatR.IMediator

    //   logger:
    //     Instância do Microsoft.Extensions.Logging.ILogger`1
    public InMemoryBus(IMediator mediator, ILogger<InMemoryBus> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    //public Task<bool> EnviarComandoAsync(ICommand comando, CancellationToken cancellationToken = default)
    //{
    //    //_logger?.NovoComando(comando.GetType());
    //    //_logger?.DetalhesComando(comando.ToJson());
    //    return _mediator.Send(comando, cancellationToken);
    //}

    public Task<Result<TResponse>> EnviarComandoAsync<TResponse>(ICommand<TResponse> comando, CancellationToken cancellationToken = default)
    {
        //_logger?.NovoComando(comando.GetType());
        //_logger?.DetalhesComando(comando.ToJson());
        return _mediator.Send(comando, cancellationToken);
    }

    public Task<Result<IEnumerable<TResponse>>> ExecutarQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        //_logger?.NovaQuery(query.GetType());
        //_logger?.DetalhesQuery(query.ToJson());
        return _mediator.Send((IRequest<Result<IEnumerable<TResponse>>>)query, cancellationToken);
    }

    //public Task<Result<IEnumerable<TResponse>>> ExecutarQueryAsync<TEntity, TResponse>(IQuery<TEntity, TResponse> query, CancellationToken cancellationToken = default) where TEntity : class
    //{
    //    //_logger?.NovaQuery(query.GetType());
    //    //_logger?.DetalhesQuery(query.ToJson());
    //    return _mediator.Send((IRequest<Result<IEnumerable<TResponse>>>)query, cancellationToken);
    //}
}
