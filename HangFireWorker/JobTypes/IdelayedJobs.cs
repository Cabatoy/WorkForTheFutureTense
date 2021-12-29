using Hangfire;
using HangFireWorker.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireWorker.JobTypes
{
    /// <summary>
    /// Oluşturulduktan belirli bir (ayarlanan) zaman sonra  
    /// sadece tek seferliğine çalışan job türüdür.
    /// </summary>
    public interface IdelayedJobs
    {
        Task SetDelayedJobsAsync();
        
        void SetDelayedJobs();
    }
}
