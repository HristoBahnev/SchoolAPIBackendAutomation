using RestSharp;

namespace BackEndAutomation.Rest
{
    public class AdminApi : BaseApiService
    {
        public AdminApi() : base()
        {
        }

        public RestResponse CreateUser(string token, string username, string password, string fullName, string role)
        {
            var request = new RestRequest("/admin/create_user", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(new
            {
                username,
                password,
                full_name = fullName,
                role
            });
            return ExecuteRequest(request);
        }

        public RestResponse ConnectParentToStudent(string token, string parentUsername, string studentId)
        {
            var request = new RestRequest("/admin/connect_parent_to_student", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(new
            {
                parent_username = parentUsername,
                student_id = studentId
            });
            return ExecuteRequest(request);
        }
    }
}
