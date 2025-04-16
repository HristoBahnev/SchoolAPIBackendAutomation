using RestSharp;

namespace BackEndAutomation.Rest
{
    public class AuthService : BaseApiService
    {
        public AuthService() : base() { }

        public RestResponse Login(string username, string password, string grantType = "password")
        {
            var request = new RestRequest("/auth/login", Method.Post);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", grantType);
            return ExecuteRequest(request);
        }
    }
}
