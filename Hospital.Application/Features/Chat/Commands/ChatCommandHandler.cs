using MediatR;
using Hospital.Application.Interfaces;

//---------------------------Note--------------------------------------
//                      --------------
// API Key in appsetting.json need to intialze before use (from OpenRouter.ai)
//                      --------------
//---------------------------------------------------------------------

namespace Hospital.Application.Features.Chat.Commands
{
    public class ChatCommandHandler : IRequestHandler<ChatCommand, string>
    {
        private readonly IAIChatService _aiChatService;

        public ChatCommandHandler(IAIChatService aiChatService)
        {
            _aiChatService = aiChatService;
        }

        public async Task<string> Handle(ChatCommand request, CancellationToken cancellationToken)
        {
            
            var aiSuggestion = await _aiChatService.GetTriageSuggestion(request.Message);
            return aiSuggestion;
        }
    }
}