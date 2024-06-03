using SharedKernel;

namespace Application.Abstractions.Messaging;
public interface IMediatorHandler
{
    ////
    //// Summary:
    ////     Envia um comando para ser tratado pelo seu respectivo handler
    ////
    //// Parameters:
    ////   comando:
    ////     Comando
    ////
    ////   cancellationToken:
    ////     Token para o cancelamento da operação
    ////
    //// Returns:
    ////     true caso o comando seja enviado, false caso contrário
    //Task<bool> EnviarComandoAsync(ICommand comando, CancellationToken cancellationToken = default);

    //
    // Summary:
    //     Envia um comando para ser tratado pelo seu respectivo handler
    //
    // Parameters:
    //   comando:
    //     Comando
    //
    //   cancellationToken:
    //     Token para o cancelamento da operação
    //
    // Type parameters:
    //   TResponse:
    //     Tipo do retorno
    //
    // Returns:
    //     Objeto de EAuditoria.Base.Classes.Resposta`1
    Task<Result<TResponse>> EnviarComandoAsync<TResponse>(ICommand<TResponse> comando, CancellationToken cancellationToken = default);

    //
    // Summary:
    //     Envia a query para ser tratado pelo seu respectivo handler
    //
    // Parameters:
    //   query:
    //     Query
    //
    //   cancellationToken:
    //     Token para o cancelamento da operação
    //
    // Type parameters:
    //   TRetorno:
    //     Tipo do retorno
    //
    // Returns:
    //     Objeto de EAuditoria.Base.Classes.Resposta`1
    Task<Result<IEnumerable<TResponse>>> ExecutarQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);

    ////
    //// Summary:
    ////     Envia a query para ser tratado pelo seu respectivo handler
    ////
    //// Parameters:
    ////   query:
    ////     Query
    ////
    ////   cancellationToken:
    ////     Token para o cancelamento da operação
    ////
    //// Type parameters:
    ////   TEntity:
    ////     Tipo da entidade filtrada
    ////
    ////   TResponse:
    ////     Tipo do retorno
    ////
    //// Returns:
    ////     Objeto de EAuditoria.Base.Classes.Resposta`1
    //Task<Result<IEnumerable<TResponse>>> ExecutarQueryAsync<TEntity, TResponse>(IQuery<TEntity, TResponse> query, CancellationToken cancellationToken = default) where TEntity : class;
}
