using Hangfire;
using HangFireWorker.JobTypes;
using System;
using System.Threading.Tasks;

namespace HangFireWorker.JobsManager
{
    public class DelayedJobsManager :IdelayedJobs
    {
        [Obsolete]
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
        public async Task SetDelayedJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
