using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Rest;
using Reqnroll;
using RestSharp;
using System.Net;
using BackEndAutomation.Utilities;
using AventStack.ExtentReports.Gherkin.Model;
using static BackEndAutomation.Utilities.UtilitiesMethods;

namespace BackEndAutomation.StepDefinitions
{
    [Binding]
    public class CreateUserStepsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly AuthService _authService;
        private readonly AdminApi _adminApi;
        private readonly ResponseDataExtractors _responseDataExtractors;

        public CreateUserStepsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _authService = new AuthService();
            _adminApi = new AdminApi();
            _responseDataExtractors = new ResponseDataExtractors();
        }

        [Given(@"I am logged in with username ""(.*)"" and password ""(.*)""")]
        public void GivenIAmAuthenticated(string username, string password)
        {
            UtilitiesMethods.LogMessage($"Logging in as: {username}", _scenarioContext);

            var response = _authService.Login(username, password);
            UtilitiesMethods.LogMessage("Login response content: " + response.Content, _scenarioContext, LogStatuses.Debug);

            var token = _responseDataExtractors.ExtractValueFromJson(response.Content, "access_token");
            _scenarioContext["token"] = token;

            UtilitiesMethods.LogMessage("Authentication successful. Token retrieved.", _scenarioContext);
        }

        [When(@"I create user with unique username ""(.*)"", password ""(.*)"", and role ""(.*)""")]
        public void WhenICreateUserWithUniqueUsername(string username, string password, string role)
        {
            UtilitiesMethods.LogMessage($"Creating user with username: {username}, role: {role}", _scenarioContext);

            string uniqueUsername = username + UtilitiesMethods.GenerateUniqueId();
            _scenarioContext["unique_username"] = uniqueUsername;
            _scenarioContext["role"] = role;

            var token = _scenarioContext["token"].ToString();
            var response = _adminApi.CreateUser(token, uniqueUsername, password, role);
            _scenarioContext["CreateUserResponse"] = response;

            UtilitiesMethods.LogMessage("Create user response content: " + response.Content, _scenarioContext, LogStatuses.Debug);
            UtilitiesMethods.LogApiError(response, _scenarioContext);
        }

        [When(@"I create user with username ""(.*)"", password ""(.*)"", and role ""(.*)""")]
        public void WhenICreateUser(string username, string password, string role)
        {
            UtilitiesMethods.LogMessage($"Creating user with username: {username}, role: {role}", _scenarioContext);
            _scenarioContext["role"] = role;

            var token = _scenarioContext["token"].ToString();
            var response = _adminApi.CreateUser(token, username, password, role);
            _scenarioContext["CreateUserResponse"] = response;

            UtilitiesMethods.LogMessage("Create user response content: " + response.Content, _scenarioContext, LogStatuses.Debug);
            UtilitiesMethods.LogApiError(response, _scenarioContext);
        }

        [Then(@"the user should be created successfully")]
        public void ThenUserShouldBeCreated()
        {
            var response = (RestResponse)_scenarioContext["CreateUserResponse"];
            var username = _scenarioContext["unique_username"];
            var role = _scenarioContext["role"];
            UtilitiesMethods.AssertEqual(HttpStatusCode.OK, response.StatusCode, "User creation failed", _scenarioContext);

            var message = _responseDataExtractors.ExtractValueFromJson(response.Content, "message");
            UtilitiesMethods.AssertEqual($"{role} '{username}' created successfully", message, "Unexpected success message", _scenarioContext);

            UtilitiesMethods.LogMessage("User created successfully.", _scenarioContext, LogStatuses.Info);
        }

        [Then("the user creation should fail with message {string}")]
        public void ThenTheUserCreationShouldFailWithMessage(string expectedMessage)
        {
            var response = (RestResponse)_scenarioContext["CreateUserResponse"];
            UtilitiesMethods.AssertTrue(response.StatusCode != HttpStatusCode.OK, "Request should not return 200 OK", _scenarioContext);

            var actualMessage = _responseDataExtractors.ExtractValueFromJson(response.Content, "detail");
            UtilitiesMethods.AssertEqual(expectedMessage, actualMessage, $"Expected error message not found. Expected to find: '{expectedMessage}'", _scenarioContext);

            UtilitiesMethods.LogMessage($"Verified expected failure message: '{expectedMessage}'", _scenarioContext);
        }

        [Then("I should receive response with status 'forbidden' and an error message {string}")]
        public void ThenIShouldReceiveResponseWithStatusForbiddenAndAnErrorMessage(string expectedMessage)
        {
            var response = _scenarioContext.Values.OfType<RestResponse>().LastOrDefault();

            UtilitiesMethods.AssertEqual(response.StatusCode, HttpStatusCode.Forbidden, "Response is not 401(Forbidden)", _scenarioContext);

            var actualMessage = _responseDataExtractors.ExtractValueFromJson(response.Content, "detail");
            UtilitiesMethods.AssertEqual(expectedMessage, actualMessage, "Unexpected success message", _scenarioContext);

            UtilitiesMethods.LogMessage($"Verified expected failure message: '{expectedMessage}'", _scenarioContext);
        }

    }

}
