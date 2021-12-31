
using System.Threading.Tasks;

namespace HangFireWorker.JobTypes
{
    /// <summary>
    /// Çok kez (tekrarlı işler) ve belirtilmiş CRON süresince çalışır
    /// </summary>
    public interface IrecurringJobs
    {
        Task SetRecurringJobsAsync();

         void SetRecurringJobs();
    }
}
