using TechTalk.SpecFlow;

namespace Shrinkr.Specifications.Steps
{
    [Binding]
    public sealed class ShrinkrStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public ShrinkrStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
    }
}
