using System;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Infrastructure;

namespace TechTalk.SpecFlow
{
    public class StepDefinition
    {
        public StepDefinitionType Type { get; }
        public string Text { get; }
        public Table Table { get; }
        public string MultilineText { get; }

        internal Action<ITestExecutionEngine> Action { get; private set; }

        internal StepDefinition(Action<ITestExecutionEngine> action)
        {
            Action = action;
        }

        public StepDefinition(StepDefinitionType type, StepDefinitionKeyword stepDefinitionKeyword, string text, Table table, string multilineText, string keyword)
        {
            Type = type;
            Text = text;
            Table = table;
            MultilineText = multilineText;
            Action = e => e.Step(stepDefinitionKeyword, keyword, text, multilineText, table);
        }
    }
}