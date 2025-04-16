using RestSharp;

namespace BackEndAutomation.Rest
{
    public class StudentApi : BaseApiService
    {
        public StudentApi() : base() { }

        public RestResponse GetStudentGrades(string token, string studentId)
        {
            var request = new RestRequest($"/students/{studentId}/grades", Method.Get);
            return ExecuteRequest(request, token);
        }
    }
}
