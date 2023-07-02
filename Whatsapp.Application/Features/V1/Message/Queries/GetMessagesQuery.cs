using MediatR;
using Shared.SeedWork;
using Whatsapp.Application.Common.Models;

namespace Whatsapp.Application.Features.V1.Message;

public sealed record GetMessagesQuery(Guid ChatId) : IRequest<ApiResult<List<MessageDto>>>;