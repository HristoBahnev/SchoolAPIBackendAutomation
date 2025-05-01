using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Rest;
using Reqnroll;
using BackEndAutomation.Utilities;
using RestSharp;
using System.Net;


namespace BackEndAutomation.Tests.StepDefinitions
{
    [Binding]
    public class TeacherActionsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly AuthService _authService;
        private readonly TeacherApi _teacherApi;
        private readonly ResponseDataExtractors _dataHelper;

        public TeacherActionsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _authService = new AuthService();
            _teacherApi = new TeacherApi();
            _dataHelper = new ResponseDataExtractors();
        }

        [When("I add grade {int} in subject {string} to student with ID {string}")]
        public void WhenIAddGradeInSubjectToStudentWithID(int grade, string subject, string studentId)
        {
            var token = _scenarioContext["token"].ToString();
            var response = _teacherApi.AddGrade(token, studentId, subject, grade);
            _scenarioContext["addGradeResponse"] = response;
        }

        [Then("the grade should be added successfully")]
        public void ThenTheGradeShouldBeAddedSuccessfully()
        {
            var response = (RestResponse)_scenarioContext["addGradeResponse"];
            UtilitiesMethods.AssertEqual(HttpStatusCode.OK, response.StatusCode, "Adding grade failed", _scenarioContext);

            var message = _dataHelper.ExtractValueFromJson(response.Content, "message");
            UtilitiesMethods.AssertEqual("Grade updated", message, "Unexpected success message", _scenarioContext);
        }

        [Then("the request should fail with message {string}")]
        public void ThenTheRequestShouldFailWithMessage(string expectedMessage)
        {
            var response = _scenarioContext.Values
                .OfType<RestResponse>()
                .LastOrDefault();

            UtilitiesMethods.AssertTrue(response.StatusCode != HttpStatusCode.OK,
                $"Expected request to fail, but got {response.StatusCode}.", _scenarioContext);

            var actualMessage = new ResponseDataExtractors().ExtractValueFromJson(response.Content, "detail");

            UtilitiesMethods.AssertEqual(expectedMessage, actualMessage, "Unexpected error message.", _scenarioContext);

            UtilitiesMethods.LogMessage($"Verified error message: '{expectedMessage}'", _scenarioContext);
        }
    }
}
