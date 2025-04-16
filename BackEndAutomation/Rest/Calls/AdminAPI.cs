using RestSharp;

namespace BackEndAutomation.Rest
{
    public class AdminApi : BaseApiService
    {
        public AdminApi() : base()
        {
        }

        public RestResponse CreateUser(string token, string username, string password, string role)
        {
            var request = new RestRequest("/users/create", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddQueryParameter("username", username);
            request.AddQueryParameter("password", password);
            request.AddQueryParameter("role", role);
            return ExecuteRequest(request);
        }

        public RestResponse ConnectParentToStudent(string token, string parentUsername, string studentId)
        {
            var request = new RestRequest("/users/connect_parent", Method.Put);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddQueryParameter("parent_username", parentUsername);
            request.AddQueryParameter("student_id", studentId);
            return ExecuteRequest(request);
        }
    }
}
