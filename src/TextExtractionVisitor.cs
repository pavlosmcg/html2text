using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Html2Text
{
    public class TextExtractionVisitor : HTMLParserBaseVisitor<StringBuilder>
    {
        public new StringBuilder AggregateResult(StringBuilder aggregate, StringBuilder nextResult)
        {
            if (aggregate == null)
                return nextResult;
            if (nextResult == null)
                return aggregate;
            return aggregate.Append(nextResult);
        }
        
        public override StringBuilder VisitChildren(IRuleNode node)
        {
            StringBuilder result = DefaultResult;
            int childCount = node.ChildCount;
            for (int i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                StringBuilder nextResult = node.GetChild(i).Accept(this);
                result = AggregateResult(result, nextResult);
            }
            return result;
        }

        public override StringBuilder VisitHtmlElement([NotNull] HTMLParser.HtmlElementContext context)
        {
            var token = context?.TAG_NAME(0)?.Payload as CommonToken;
            var tagName = token?.Text?.Trim();
            bool needsNewLine = NeedsNewLine(tagName);
            var innerResult = VisitChildren(context);
            return needsNewLine ? innerResult.AppendLine() : innerResult;
        }

        public override StringBuilder VisitHtmlChardata([NotNull] HTMLParser.HtmlChardataContext context)
        {
            var token = context?.HTML_TEXT()?.Payload as CommonToken;
            var value = token?.Text;
            return string.IsNullOrWhiteSpace(value) ? null : new StringBuilder(value);
        }

        private static bool NeedsNewLine(string tagName)
        {
            switch (tagName)
            {
                case "li":
                case "p":
                case "br":
                    return true;
                default:
                    return false;
            }
        }

        
    }
}
