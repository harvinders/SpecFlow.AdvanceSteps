using TechTalk.SpecFlow.Bindings;

namespace TechTalk.SpecFlow
{
    public class StepDefinition
    {
        public StepDefinitionType Type { get; }
        public string Text { get; }
        public Table Table { get; }
        public string MultilineText { get; }

        public StepDefinition(StepDefinitionType type, string text, Table table, string multilineText)
        {
            Type = type;
            Text = text;
            Table = table;
            MultilineText = multilineText;
        }
    }
}