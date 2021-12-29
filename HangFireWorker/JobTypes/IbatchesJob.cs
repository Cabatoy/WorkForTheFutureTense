
using System.Threading.Tasks;

namespace HangFireWorker.JobTypes
{
    public interface IbatchesJob
    {
       
        Task SetBatcJobsAsync();


       
        void SetBatcJobs();
    }
}
