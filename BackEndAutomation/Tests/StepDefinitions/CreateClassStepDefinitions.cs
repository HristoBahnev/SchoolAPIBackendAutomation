using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Rest;
using Reqnroll;
using BackEndAutomation.Utilities;
using RestSharp;
using System.Net;

namespace BackEndAutomation.Tests.StepDefinitions
{
    [Binding]
    public class CreateClassStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly AuthService _authService;
        private readonly TeacherApi _teacherApi;
        private readonly ResponseDataExtractors _dataHelper;

        public CreateClassStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _authService = new AuthService();
            _teacherApi = new TeacherApi();
            _dataHelper = new ResponseDataExtractors();
        }

        [When("I create a class with name {string} and subjects {string}")]
        public void WhenICreateAClassWithNameAndSubjects(string className, string subjects)
        {
            var token = _scenarioContext["token"].ToString();
            var response = _teacherApi.CreateClass(token, className, subjects);
            _scenarioContext["createClassResponse"] = response;
        }

        [Then("the class should be created successfully")]
        public void ThenTheClassShouldBeCreatedSuccessfully()
        {
            var response = (RestResponse)_scenarioContext["createClassResponse"];
            UtilitiesMethods.AssertEqual(HttpStatusCode.OK, response.StatusCode, "Class creation failed", _scenarioContext);

            var message = _dataHelper.ExtractValueFromJson(response.Content, "message");
            UtilitiesMethods.AssertEqual("Class created", message, "Unexpected success message", _scenarioContext);
        }
    }
}
