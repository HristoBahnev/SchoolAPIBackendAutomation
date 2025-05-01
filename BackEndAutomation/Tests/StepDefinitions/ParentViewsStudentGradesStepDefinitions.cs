using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Rest;
using Reqnroll;
using BackEndAutomation.Utilities;
using static BackEndAutomation.Utilities.UtilitiesMethods;
using RestSharp;
using System.Net;

namespace BackEndAutomation.Tests.StepDefinitions
{
    [Binding]
    public class ParentViewsStudentGradesStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly ParentApi _parentApi;
        private readonly ResponseDataExtractors _dataHelper;

        public ParentViewsStudentGradesStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _parentApi = new ParentApi();
            _dataHelper = new ResponseDataExtractors();
        }

        [When("I view grades for student with ID {string}")]
        public void WhenIViewGradesForStudentWithID(string studentId)
        {
            var token = _scenarioContext["token"].ToString();
            var response = _parentApi.ViewStudentGrade(token, studentId);
            _scenarioContext["viewGradesResponse"] = response;

            UtilitiesMethods.LogMessage($"Parent requests grades for student ID '{studentId}'", _scenarioContext);
            UtilitiesMethods.LogMessage("Response content: " + response.Content, _scenarioContext, LogStatuses.Debug);
        }

        [Then("the grades should be displayed successfully")]
        public void ThenTheGradesShouldBeDisplayedSuccessfully()
        {
            var response = (RestResponse)_scenarioContext["viewGradesResponse"];

            UtilitiesMethods.AssertEqual(HttpStatusCode.OK, response.StatusCode, "Failed to view grades.", _scenarioContext);

            UtilitiesMethods.LogMessage("Grades displayed successfully.", _scenarioContext);
        }

        [Then("the grades should be not be displayed and an error message {string} is displayed")]
        public void ThenTheGradesShouldBeNotBeDisplayedAndAnErrorMessageIsDisplayed(string expectedMessage)
        {
            var response = (RestResponse)_scenarioContext["viewGradesResponse"];

            UtilitiesMethods.AssertTrue(response.StatusCode != HttpStatusCode.OK, $"Expected non successfull response, but got response {response.StatusCode}", _scenarioContext);

            var actualMessage = _dataHelper.ExtractValueFromJson(response.Content, "detail");

            UtilitiesMethods.AssertEqual(expectedMessage, actualMessage, "Unexpected error message", _scenarioContext);

            UtilitiesMethods.LogMessage($"Verified response: '{actualMessage}'", _scenarioContext);
        }

    }
}
