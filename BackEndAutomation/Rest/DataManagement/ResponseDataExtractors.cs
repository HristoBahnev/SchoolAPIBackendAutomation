using BackEndAutomation.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEndAutomation.Rest.DataManagement
{
    public class ResponseDataExtractors
    {
        public string ExtractValueFromJson(string jsonResponse, string jsonIdentifier)
        {
            try
            {
                JObject jsonObject = JObject.Parse(jsonResponse);
                return jsonObject[jsonIdentifier]?.ToString();
            }
            catch (JsonReaderException ex)
            {
                Logger.Log.Error($"Error parsing API response: {ex.Message}");
                Logger.Log.Debug($"Raw response: {jsonResponse}");
                throw new InvalidOperationException("Failed to parse the API response", ex);
            }
        }
    }
}
