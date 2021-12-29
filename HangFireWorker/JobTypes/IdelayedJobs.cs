
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
