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
    public class BatchesJobManager :IbatchesJob
    {
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public void SetBatcJobs()
        {
           
        }
        [Obsolete]
        [AutomaticRetry(Attempts = 2,OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        [LogCompletion]
        public async Task SetBatcJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
