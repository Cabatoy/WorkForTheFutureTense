using Hangfire;
using HangFireWorker.JobTypes;
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
        public void SetBatcJobs()
        {
           
        }
        [Obsolete]
        public async Task SetBatcJobsAsync()
        {
            await Task.Run(() =>
            {

            });
        }
        
    }

}
