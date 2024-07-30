using Hangfire;
using Hangfire.Server;
using ReportImportExport.Base;

namespace MyFinances.Utils
{
    public abstract class DefaultQueueJob<T> : ExceptionlessJobBase<T>
    {
        private const string DEFAULT_QUEUE_NAME = "default";

        protected DefaultQueueJob()
        {
        }

        [Queue(DEFAULT_QUEUE_NAME)]
        public new void Execute(PerformContext context, T args)
        {
            base.Execute(context, args);
        }
    }
}
