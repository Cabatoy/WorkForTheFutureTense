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
    public class ContinuationJobsManager :IcontinuationJobs
    {
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
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
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public void SetContinuationJobs(string jobId)
        {
            BackgroundJob.ContinueJobWith(jobId,() => Console.WriteLine("Continuation!"));
        }
     
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public async Task SetContinuationJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
