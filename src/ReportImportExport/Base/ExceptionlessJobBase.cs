namespace ReportImportExport.Base
{
    public abstract class ExceptionlessJobBase<T> : HangFireJobBase<T>
    {
        public void ExecuteWithExceptionless(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
