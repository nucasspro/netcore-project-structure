using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Whatsapp.Application.Common.Models;
using Whatsapp.Application.Features.V1.Message;

namespace Whatsapp.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class MessageController : Controller
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private static class RouteNames
    {
        public const string GetMessages = nameof(GetMessages);
        public const string GetMessage = nameof(GetMessage);
        public const string CreateMessage = nameof(CreateMessage);
        public const string UpdateMessage = nameof(UpdateMessage);
        public const string DeleteMessage = nameof(DeleteMessage);
    }
    
    [HttpGet("{chatId:guid}", Name = RouteNames.GetMessages)]
    [ProducesResponseType(typeof(IEnumerable<MessageDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessages([Required] Guid chatId)
    {
        var query = new GetMessagesQuery(chatId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}