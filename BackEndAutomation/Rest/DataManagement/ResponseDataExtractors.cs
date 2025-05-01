using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEndAutomation.Rest.DataManagement
{
    public static class ResponseDataExtractors
    {
        public static string ExtractValueFromJson(string jsonResponse, string jsonIdentifier)
        {
            try
            {
                JObject jObj = JObject.Parse(jsonResponse);
                return jObj.SelectToken(jsonIdentifier)?.ToString();
            }
            catch (JsonReaderException ex)
            {
                Console.Error.WriteLine($"Error parsing API response: {ex.Message}");
                Console.Error.WriteLine($"Raw response: {jsonResponse}");
                throw new InvalidOperationException("Failed to parse the API response. Please verify the API status and response format.", ex);
            }
        }
    }
}
