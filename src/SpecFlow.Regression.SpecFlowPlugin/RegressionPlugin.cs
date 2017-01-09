using SpecFlow.Regression;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(RegressionPlugin))]

namespace SpecFlow.Regression
{
    public class RegressionPlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters)
        {
            runtimePluginEvents.CustomizeTestThreadDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterTypeAs<RegressionTestRunner, ITestRunner>();
            };
        }
    }
}