using BackEndAutomation.Utilities;
using RestSharp;

namespace BackEndAutomation.Rest
{
    public class AdminApi : BaseApiService
    {
        public AdminApi() : base() { }

        public RestResponse CreateUser(string token, string username, string password, string role)
        {
            var resource = $"/users/create?username={username}&password={password}&role={role}";
            var request = new RestRequest(resource, Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");

            return ExecuteRequest(request);
        }

        public RestResponse ConnectParentToStudent(string token, string parentUsername, string studentId)
        {
            var resource = $"/users/connect_parent?parent_username={parentUsername}&student_id={studentId}";
            var request = new RestRequest(resource, Method.Put);
            request.AddHeader("Authorization", $"Bearer {token}");

            return ExecuteRequest(request);
        }
    }
}
