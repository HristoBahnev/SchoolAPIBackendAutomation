using Newtonsoft.Json.Linq;

namespace BackEndAutomation.Rest.DataManagement
{
    public class ResponseDataExtractors
    {
        public string ExtractValueFromJson(string jsonResponse, string jsonIdentifier)
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[jsonIdentifier]?.ToString();
        }
    }
}
