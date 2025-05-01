using RestSharp;

namespace BackEndAutomation.Rest
{
    public class TeacherApi : BaseApiService
    {
        public TeacherApi() : base()
        {
        }

        public RestResponse CreateClass(string token, string className, string subjects)
        {
            var request = new RestRequest("/classes/create", Method.Post);
            request.AddQueryParameter("class_name", className);
            var subjectList = subjects.Split(",");
            for (int i = 0; i < subjectList.Length; i++)
            {
                request.AddQueryParameter($"subject_{i + 1}", subjectList[i].Trim());
            }
            return ExecuteRequest(request, token);
        }
        public RestResponse AddGrade(string token, string studentId, string subject, int grade)
        {
            var request = new RestRequest("/grades/add", Method.Put);
            request.AddQueryParameter("student_id", studentId);
            request.AddQueryParameter("subject", subject);
            request.AddQueryParameter("grade", grade);
            return ExecuteRequest(request, token);
        }
    }
}
