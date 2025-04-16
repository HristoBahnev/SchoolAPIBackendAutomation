using RestSharp;

namespace BackEndAutomation.Rest
{
    public abstract class BaseApiService
    {
        protected readonly RestClient _client;
        protected static readonly string BaseURL = "https://schoolprojectapi.onrender.com";
        public BaseApiService()
        {
            _client = new RestClient(BaseURL);
        }

        protected RestResponse ExecuteRequest(RestRequest request, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }
            var response = _client.Execute(request);

            return response;
        }
    }
}
