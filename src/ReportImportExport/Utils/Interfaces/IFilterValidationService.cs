using System.Text.Json;

namespace ReportImportExport.Utils
{
    public interface IFilterValidationService
    {
        void RunValidations(JsonElement input, int inputType);
    }
}
