using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using Serilog;
using System;

namespace SerilogSeqLogger.LogCompletion
{
    public class LogCompletionAttribute :JobFilterAttribute, IClientFilter, IServerFilter, IElectStateFilter, IApplyStateFilter
    {
        //private static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(LogCompletionAttribute));
        private static readonly ILogger log = new LoggerConfiguration().WriteTo.Seq("http://localhost:5341").CreateLogger();
        //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LogCompletionAttribute()
        {

        }

        [Obsolete]
        public void OnPerforming(PerformingContext filterContext)
        {
            // log.Info(filterContext);
            // throw new NotImplementedException();
            // calisiyor
        }

        [Obsolete]
        public void OnPerformed(PerformedContext filterContext)
        {
            if (!filterContext.Canceled && filterContext.Exception != null)
            {
                string name = filterContext.Job.ToString() +"||"+ filterContext.Job.Method.Name;
                

                log.Error(name,filterContext.Exception);
                //log.Error(filterContext,filterContext.Exception);
                // Here you would write to your database.

                // loglama
            }
        }
        [Obsolete]
        public void OnCreating(CreatingContext context)
        {
            // log.InfoFormat("Creating a job based on method `{0}`...",context.Job.Method.Name);
        }
        [Obsolete]
        public void OnCreated(CreatedContext context)
        {
            //log.InfoFormat(
            //    "Job that is based on method `{0}` has been created with id `{1}`",
            //    context.Job.Method.Name,
            //    context.BackgroundJob?.Id);
        }
        [Obsolete]
        public void OnStateElection(ElectStateContext context)
        {
            var failedState = context.CandidateState as FailedState;
            if (failedState != null)
            {
                //log.WarnFormat(
                //    "Job `{0}` has been failed due to an exception `{1}`",
                //    context.BackgroundJob.Id,
                //    failedState.Exception);
                log.Warning("`{0}` Hata Aldi. Hata===>`{1}` ",
                    context.BackgroundJob.Job.Method.Name,
                    failedState.Exception);
            }
        }
        [Obsolete]
        public void OnStateApplied(ApplyStateContext context,IWriteOnlyTransaction transaction)
        {
            //Logger.InfoFormat(
            //    "Job `{0}` state was changed from `{1}` to `{2}`",
            //    context.BackgroundJob.Id,
            //    context.OldStateName,
            //    context.NewState.Name);
        }
        [Obsolete]
        public void OnStateUnapplied(ApplyStateContext context,IWriteOnlyTransaction transaction)
        {
            //Logger.InfoFormat(
            //    "Job `{0}` state `{1}` was unapplied.",
            //    context.BackgroundJob.Id,
            //    context.OldStateName);
        }
    }
}
