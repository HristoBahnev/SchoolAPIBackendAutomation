using RestSharp;

namespace BackEndAutomation.Rest
{
    public class ParentApi : BaseApiService
    {
        public ParentApi() : base()
        {
        }

        public RestResponse ViewStudentGrade(string token, string studentId)
        {
            var request = new RestRequest($"/grades/student/{studentId}", Method.Get);
            return ExecuteRequest(request, token);
        }
    }
}
