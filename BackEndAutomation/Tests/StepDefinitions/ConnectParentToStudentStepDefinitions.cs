using System.Net;
using BackEndAutomation.Rest;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Reqnroll;
using RestSharp;
using static BackEndAutomation.Utilities.UtilitiesMethods;

namespace BackEndAutomation.Tests.StepDefinitions
{
    [Binding]
    public class ConnectParentToStudentStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public ConnectParentToStudentStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }

        [When("I connect parent {string} to student with ID {string}")]
        public void WhenIConnectParentToStudent(string parentUsername, string studentId)
        {
            var token = _scenarioContext["token"].ToString();

            var response = new AdminApi().ConnectParentToStudent(token, parentUsername, studentId);
            _scenarioContext["connectParentResponse"] = response;

            UtilitiesMethods.LogMessage($"Connecting parent '{parentUsername}' to student '{studentId}'", _scenarioContext, LogStatuses.Info);
            UtilitiesMethods.LogMessage("Response content: " + response.Content, _scenarioContext, LogStatuses.Debug);
        }

        [Then("the parent should be connected successfully")]
        public void ThenTheParentShouldBeConnectedSuccessfully()
        {
            var response = (RestResponse)_scenarioContext["connectParentResponse"];
            var dataHelper = new ResponseDataExtractors();

            UtilitiesMethods.AssertEqual(HttpStatusCode.OK, response.StatusCode, "Connecting parent with students failed.", _scenarioContext);

            var message = dataHelper.ExtractValueFromJson(response.Content, "message");

            UtilitiesMethods.AssertEqual("Parent linked to student", message, "Unexpected response message.", _scenarioContext);

            UtilitiesMethods.LogMessage("Parent linked to student", _scenarioContext);
        }
    }
}
