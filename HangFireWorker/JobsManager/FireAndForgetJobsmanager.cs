using Hangfire;
using HangFireWorker.Jobs;
using HangFireWorker.JobTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireWorker.JobsManager
{
    internal class FireAndForgetJobsmanager :IfireAndForgetJobs
    {
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public void SetFireAndForgetJobs()
        {
            var jobId = BackgroundJob.Enqueue( () => Console.WriteLine("Fire-and-forget!"));
            //Hangfire.BackgroundJob.Enqueue<CurrencyScheduleJobManager>(
            //            job => job.Process()
            //            );

        }
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public async Task SetFireAndForgetJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
