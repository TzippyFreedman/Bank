using NServiceBus;
using NServiceBus.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.NServiceBus
{
    public static class UserServiceRetryPolicy
    {
        public static RecoverabilityAction UserServiceRetryPolicyInvoke(RecoverabilityConfig config, ErrorContext context)
        {
            // invocation of default recoverability policy
            var action = DefaultRecoverabilityPolicy.Invoke(config, context);

            if (!(action is DelayedRetry delayedRetryAction))
            {
                return action;
            }
            if (context.Exception is NullReferenceException)
            {
                return RecoverabilityAction.Discard("Business operation timed out.");
            }
            //if (context.Exception is PatientNotExistExcption)
            //{
            //    return RecoverabilityAction.MoveToError(config.Failed.ErrorQueue);
            //}
            // Override default delivery delay.
            return RecoverabilityAction.DelayedRetry(TimeSpan.FromMinutes(3));

            // If it does not make sense to have this message around anymore
            // it can be discarded with a reason.


            // override delayed retry decision for custom exception
            // i.e. MyOtherBusinessException should do fixed backoff of 5 seconds
            //if (context.Exception is MyOtherBusinessException &&
            //    context.DelayedDeliveriesPerformed <= config.Delayed.MaxNumberOfRetries)
            //{
            //    return RecoverabilityAction.DelayedRetry(TimeSpan.FromSeconds(5));
            //}

            // in all other cases No Immediate or Delayed Retries, go to default error queue

        }

    }
}
