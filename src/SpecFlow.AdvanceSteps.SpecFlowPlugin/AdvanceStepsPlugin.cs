using SpecFlow.AdvanceSteps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(AdvanceStepsPlugin))]

namespace SpecFlow.AdvanceSteps
{
    public class AdvanceStepsPlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters)
        {
            runtimePluginEvents.CustomizeTestThreadDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterTypeAs<TestRunner, ITestRunner>();
            };
        }
    }
}