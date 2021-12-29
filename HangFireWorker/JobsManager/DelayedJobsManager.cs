using Hangfire;
using HangFireWorker.JobTypes;
using HangFireWorker.Logs;
using System;
using System.Threading.Tasks;

namespace HangFireWorker.JobsManager
{
    public class DelayedJobsManager :IdelayedJobs
    {
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public void SetDelayedJobs()
        {
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"),TimeSpan.FromDays(7));
            //Hangfire.BackgroundJob.Schedule<UserRegisterScheduleJobManager>
            //     (
            //      job => job.Process(userId),
            //      TimeSpan.FromSeconds(10)
            //      );
        }
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public async Task SetDelayedJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
