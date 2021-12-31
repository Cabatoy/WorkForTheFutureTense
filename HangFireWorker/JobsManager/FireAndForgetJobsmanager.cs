using Hangfire;
using HangFireWorker.JobTypes;
using System;
using System.Threading.Tasks;

namespace HangFireWorker.JobsManager
{
    internal class FireAndForgetJobsmanager :IfireAndForgetJobs
    {
        [Obsolete]
        public void SetFireAndForgetJobs()
        {
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
            //Hangfire.BackgroundJob.Enqueue<CurrencyScheduleJobManager>(
            //            job => job.Process()
            //            );

        }
        [Obsolete]
        public async Task SetFireAndForgetJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
