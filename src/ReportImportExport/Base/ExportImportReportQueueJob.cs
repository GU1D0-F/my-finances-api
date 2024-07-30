using Hangfire.Server;

namespace ReportImportExport.Base
{
    public abstract class ExportImportReportQueueJob<T> : ExceptionlessJobBase<T>
    {
        public new void Execute(PerformContext context, T args)
        {
            base.Execute(context, args);
        }
    }
}
