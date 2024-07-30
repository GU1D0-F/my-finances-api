namespace ReportImportExport.Base
{
    public interface IBackgroundJob<in TArgs>
    {
        void Execute(TArgs args);
    }
}
