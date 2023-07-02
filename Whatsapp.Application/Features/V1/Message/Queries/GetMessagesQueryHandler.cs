using AutoMapper;
using MediatR;
using Serilog;
using Shared.SeedWork;
using Whatsapp.Application.Common.Interfaces;
using Whatsapp.Application.Common.Models;

namespace Whatsapp.Application.Features.V1.Message;

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, ApiResult<List<MessageDto>>>
{
    private const string MethodName = nameof(GetMessagesQueryHandler);
    
    private readonly IMapper _mapper;
    private readonly IMessageRepository _repository;
    private readonly ILogger _logger;

    public GetMessagesQueryHandler(IMapper mapper, IMessageRepository repository, ILogger logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<ApiResult<List<MessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN: {MethodName}");
        
        var messages = await _repository.GetMessagesByChatIdAsync(request.ChatId);
        var messagesDto = _mapper.Map<List<MessageDto>>(messages);
        
        
        _logger.Information($"END: {MethodName}");
        return new ApiSuccessResult<List<MessageDto>>(messagesDto); 
    }
}