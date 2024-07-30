using Newtonsoft.Json;
using System.Text.Json;

namespace ReportImportExport.Utils
{
    public class FilterValidationService : IFilterValidationService
    {
        public void RunValidations(JsonElement input, int inputType)
        {
        }

        protected virtual void TryCastFilters<T>(JsonElement filters)
        {
            try
            {
                JsonConvert.DeserializeObject<T>(filters.ToString(), new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Error,
                });

            }
            catch (Exception)
            {
                throw new Exception($"O objeto enviado [{filters}] não corresponde ao filtro esperado: [{typeof(T)}]");
            }
        }
    }
}
