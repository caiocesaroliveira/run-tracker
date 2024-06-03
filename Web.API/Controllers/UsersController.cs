using Application.Abstractions.Messaging;
using Application.Users;
using Application.Users.Create;
using Application.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediatorHandler _bus;

    public UsersController(IMediatorHandler sender)
    {
        _bus = sender;
    }

    /// <summary>
    ///     Lista de <see cref="Domain.Users.User"/>s
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da operação</param>
    /// <returns>Instância do <see cref="UserResponse"/> em caso de 
    ///     <see cref="HttpStatusCode"/> ou <see cref="HttpStatusCode.NotFound"/></returns>
    [HttpGet]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _bus.ExecutarQueryAsync(new GetUserByIdQuery(id), cancellationToken);
        return response.IsSuccess ?
            Ok(response.Value) :
            NotFound(response.Error);
    }

    /// <summary>
    ///     Cadastra um <see cref="Domain.Users.User"/>
    /// </summary>
    /// <param name="request">Dados do <see cref="Domain.Users.User"/></param>
    /// <param name="cancellationToken">Token de cancelamento da operação</param>
    /// <returns>Retorna o objeto <see cref="Result{Guid}"/> com o identificador</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody]CreateUserCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await _bus.EnviarComandoAsync(command, cancellationToken);
        return result.IsSuccess ?
            Ok(result.Value) :
            BadRequest(result.Error);
    }

   
}
