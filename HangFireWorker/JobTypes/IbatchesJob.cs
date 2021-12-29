using Hangfire;
using HangFireWorker.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireWorker.JobTypes
{
    public interface IbatchesJob
    {
       
        Task SetBatcJobsAsync();


       
        void SetBatcJobs();
    }
}
