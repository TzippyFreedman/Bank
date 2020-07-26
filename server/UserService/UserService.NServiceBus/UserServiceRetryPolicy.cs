using NServiceBus;
using NServiceBus.Transport;
using System;

namespace UserService.NServiceBus
{
    public static class UserServiceRetryPolicy
    {
        public static RecoverabilityAction UserServiceRetryPolicyInvoke(RecoverabilityConfig config, ErrorContext context)
        {
            var action = DefaultRecoverabilityPolicy.Invoke(config, context);
            if (!(action is DelayedRetry))
            {
                return action;
            }
            if (context.Exception is NullReferenceException)
            {
                return RecoverabilityAction.MoveToError(config.Failed.ErrorQueue);
            }
            return RecoverabilityAction.DelayedRetry(TimeSpan.FromMinutes(3));
        }
    }
}
