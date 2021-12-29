
using System.Threading.Tasks;

namespace HangFireWorker.JobTypes
{
    /// <summary>
    /// Bir kere ve hemen çalışan background job tipidir. 
    /// </summary>
    public interface IfireAndForgetJobs
    {
        Task SetFireAndForgetJobsAsync();

        void SetFireAndForgetJobs();
    }
}
