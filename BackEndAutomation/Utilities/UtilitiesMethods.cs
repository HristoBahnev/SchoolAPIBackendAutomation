using AventStack.ExtentReports;
using NUnit.Framework;
using Reqnroll;
using RestSharp;
using System.Net;

namespace BackEndAutomation.Utilities
{
    public static class UtilitiesMethods
    {
        public static void AssertEqual<T>(T expected, T actual, string message, ScenarioContext scenarioContext)
        {
            Assert.That(actual, Is.EqualTo(expected), message);
            LogAssertionResult(expected, actual, message, scenarioContext);
        }

        public static void AssertTrue(bool condition, string message, ScenarioContext scenarioContext)
        {
            Assert.That(condition, Is.True, message);
            LogAssertionResult(condition, true, message, scenarioContext);
        }

        private static void LogAssertionResult<T>(T expected, T actual, string message, ScenarioContext scenarioContext)
        {
            ExtentTest test = scenarioContext.Get<ExtentTest>("ExtentTest");
            Status status = expected.Equals(actual) ? Status.Pass : Status.Fail;

            test.Log(status, $"Assertion result: Expected {expected}, but was {actual}. {message}");
        }

        public static void LogMessage(string message, ScenarioContext scenarioContext, LogStatuses status = LogStatuses.Info)
        {
            ExtentTest test = scenarioContext.Get<ExtentTest>("ExtentTest");

            switch (status)
            {
                case LogStatuses.Info:
                    test.Log(Status.Info, message);
                    Logger.Log.Info(message);
                    break;
                case LogStatuses.Warning:
                    test.Log(Status.Warning, message);
                    Logger.Log.Warn(message);
                    break;
                case LogStatuses.Debug:
                    test.Log(Status.Info, message); 
                    Logger.Log.Debug(message);
                    break;
            }
        }

        public static void LogApiError(RestResponse response, ScenarioContext scenarioContext)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string errorMessage = $"API request failed with status code: {response.StatusCode}. Response: {response.Content}";
                ExtentTest test = scenarioContext.Get<ExtentTest>("ExtentTest");
                test.Log(Status.Fail, errorMessage);
                Logger.Log.Error(errorMessage);
            }
        }
    }

    public enum LogStatuses
    {
        Info,
        Warning,
        Debug
    }
}
