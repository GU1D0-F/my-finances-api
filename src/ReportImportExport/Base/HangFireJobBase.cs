using Hangfire.Server;

namespace ReportImportExport.Base
{
    public abstract class HangFireJobBase<TArgs> : BackgroundJob<TArgs>
    {
        public PerformContext Console { get; set; }

        public virtual void Execute(PerformContext context, TArgs args)
        {
            Console = context;
            Execute(args);
        }
    }
}
