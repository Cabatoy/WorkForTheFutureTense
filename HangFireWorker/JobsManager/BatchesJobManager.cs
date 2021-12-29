using Hangfire;
using HangFireWorker.JobTypes;
using HangFireWorker.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireWorker.JobsManager
{

    /// <summary>
    /// toplu şekilde işlem yürütmek için , ücretli sürüm
    /// </summary>
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
