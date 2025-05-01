using BackEndAutomation.Rest;
using BackEndAutomation.Rest.DataManagement;
using NUnit.Framework;
using Reqnroll;
using RestSharp;
using System.Net;

namespace BackEndAutomation.Tests.StepDefinitions
{
    [Binding]
    public class LoginStepsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly AuthService _authService;
        private readonly ResponseDataExtractors _responseDataExtractors;


        public LoginStepsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _authService = new AuthService();
            _responseDataExtractors = new ResponseDataExtractors();
        }

        [When(@"I send a login request with username {string} and password {string}")]
        public void WhenISendALoginRequest(string username, string password)
        {
            var response = _authService.Login(username, password);
            _scenarioContext["LoginResponse"] = response;
        }

        [Then(@"I should receive a successful response with a token")]
        public void ThenIShouldReceiveASuccessfulResponseWithAToken()
        {
            var response = (RestResponse)_scenarioContext["LoginResponse"];
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var accessToken = _responseDataExtractors.ExtractValueFromJson(response.Content, "access_token");
            Assert.That(accessToken, Is.Not.Empty);

        }

        [Then("I should receive a response with status 'unauthorized' and an error message {string}")]
        public void ThenIShouldReceiveAnUnauthorizedResponseWithAnErrorMessage(string errorMessage)
        {
            var response = _scenarioContext.Values.OfType<RestResponse>().LastOrDefault();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));

            var errorDetail = _responseDataExtractors.ExtractValueFromJson(response.Content, "detail");
            Assert.That(errorDetail, Is.EqualTo(errorMessage));

        }
    }
}