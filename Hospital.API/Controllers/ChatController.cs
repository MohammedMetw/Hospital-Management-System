using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Features.Chat.Commands;
using System.Threading.Tasks;

//---------------------------Note--------------------------------------
//                      --------------
// API Key in appsetting.json need to intialze before use (from OpenRouter.ai)
//                      --------------
//---------------------------------------------------------------------

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpPost("Chatbot")]
        public async Task<IActionResult> PostChatMessage([FromBody] ChatCommand command)
        {
            var aiResponse = await _mediator.Send(command);
            return Ok(new { response = aiResponse });
        }
    }
}