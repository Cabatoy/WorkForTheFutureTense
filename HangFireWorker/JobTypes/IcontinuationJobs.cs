
using System.Threading.Tasks;

namespace HangFireWorker.JobTypes
{
    /// <summary>
    /// Birbiri ile ilişkili işlerin olduğu zaman çalışan job. Job tetiklenmesi için başka bir job bitmesi gerekiyor
    /// </summary>
    /// <param name="id">İlişkili job id değeri</param>
    public interface IcontinuationJobs
    {
        Task SetContinuationJobsAsync();
       
        void SetContinuationJobs();
      
        void SetContinuationJobs(string Id);
    }
}
