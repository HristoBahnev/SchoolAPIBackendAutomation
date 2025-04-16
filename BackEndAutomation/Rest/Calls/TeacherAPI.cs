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

        public RestResponse AddStudentToClass(string token, string className, string studentName)
        {
            var request = new RestRequest("/classes/add_student", Method.Post);
            request.AddJsonBody(new { className, studentName });
            return ExecuteRequest(request, token);
        }

        public RestResponse AssignGrade(string token, string studentId, int grade, string subject)
        {
            var request = new RestRequest("/grades/add", Method.Put);
            request.AddJsonBody(new { student_id = studentId, grade, subject });
            return ExecuteRequest(request, token);
        }
    }
}
