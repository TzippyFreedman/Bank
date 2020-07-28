using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract;

namespace UserService.NServiceBus
{
    public class UpdateHistoryHandler : IHandleMessages<IUpdateHistory>
    {
        private readonly IUserRepository _userRepository;

        public UpdateHistoryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task Handle(IUpdateHistory message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
