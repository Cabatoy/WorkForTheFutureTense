using Hangfire;
using HangFireWorker.JobTypes;
using System;
using System.Threading.Tasks;

namespace HangFireWorker.JobsManager
{
    public class ContinuationJobsManager :IcontinuationJobs
    {
        [Obsolete]
        public void SetContinuationJobs()
        {
            var FireAndForgetId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));

            BackgroundJob.ContinueJobWith(FireAndForgetId,() => Console.WriteLine("Continuation!"));

            // var jobId = Hangfire.BackgroundJob.Enqueue<CurrencyScheduleJobManager>(
            //            job => job.Process()
            //            );
            //ContinuationJobs.GetMyFinancialCashUpdate(jobId);

            //Hangfire.BackgroundJob.ContinueJobWith<FinancialCashScheduleJobManager>(
            //               parentId: id,
            //               job => job.Process());
        }
      
        [Obsolete]
        public void SetContinuationJobs(string jobId)
        {
            BackgroundJob.ContinueJobWith(jobId,() => Console.WriteLine("Continuation!"));
        }
     
        [Obsolete]
        public async Task SetContinuationJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
