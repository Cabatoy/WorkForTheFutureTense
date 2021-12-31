
using Hangfire;
using SerilogSeqLogger.LogCompletion;
using System;

namespace SampleMethods
{
    public class DividedZeroSample
    {
        [AutomaticRetry(Attempts = 5,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public static void Execute()
        {
            throw new DivideByZeroException();
        }
    }
}
