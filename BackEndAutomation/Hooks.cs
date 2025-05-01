using BackEndAutomation.Utilities;
using Reqnroll;
using System.Collections;
using static BackEndAutomation.Utilities.UtilitiesMethods;

namespace BackEndAutomation
{
    [Binding]
    public sealed class Hooks
    {

        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void SetLogFileName()
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            NLog.LogManager.Configuration.Variables["logFileNameTimestamp"] = timestamp;
            NLog.LogManager.ReconfigExistingLoggers();

            ExtentManager.InitReport();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            string scenarioName = GetScenarioName(_scenarioContext);
            var еxtentTest = ExtentManager.CreateTest("Scenario: " + scenarioName);
            _scenarioContext["ExtentTest"] = еxtentTest;
            UtilitiesMethods.LogMessage($"Starting scenario: {scenarioName}", _scenarioContext);
            Logger.Log.Info("Starting scenario...");
        }

        [BeforeStep]
        public void BeforeStep()
        {
            UtilitiesMethods.LogMessage("Start executing Step: " + _scenarioContext.StepContext.StepInfo.Text, _scenarioContext, LogStatuses.Debug);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Logger.Log.Info("Scenario finished.");
        }

        [AfterTestRun]
        public static void FlushReport()
        {
            ExtentManager.FlushReport();
        }

        private static string GetScenarioName(ScenarioContext context)
        {
            string scenarioParameters = string.Empty;

            foreach (DictionaryEntry entry in context.ScenarioInfo.Arguments)
            {
                scenarioParameters += @"""" + entry.Value.ToString() + @"""" + ", ";
            }

            if (!string.IsNullOrEmpty(scenarioParameters))
            {
                scenarioParameters = "(" + scenarioParameters + "null)";
            }

            string scenarioName = context.ScenarioInfo.Title + scenarioParameters;

            return scenarioName;
        }
    }
}