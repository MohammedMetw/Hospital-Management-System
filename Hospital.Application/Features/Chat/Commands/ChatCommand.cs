using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.Chat.Commands
{
    public class ChatCommand : IRequest<string>
    {
        public string Message { get; set; }
    }
}
