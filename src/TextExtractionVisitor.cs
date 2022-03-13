using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Html2Text.Antlr;

namespace Html2Text
{
    public class TextExtractionVisitor : HTMLParserBaseVisitor<ResultNode>
    {
        public new ResultNode AggregateResult(ResultNode aggregate, ResultNode nextResult)
        {
            if (aggregate == null)
                return nextResult;
            if (nextResult == null)
                return aggregate;

            return aggregate.Append(nextResult);
        }

        public override ResultNode VisitChildren(IRuleNode node)
        {
            ResultNode resultNode = DefaultResult;
            int childCount = node.ChildCount;
            for (int i = 0; i < childCount && ShouldVisitNextChild(node, resultNode); ++i)
            {
                ResultNode nextResult = node.GetChild(i).Accept(this);
                resultNode = AggregateResult(resultNode, nextResult);
            }
            return resultNode;
        }

        public override ResultNode VisitHtmlElement([NotNull] HTMLParser.HtmlElementContext context)
        {
            var tagName = GetTagName(context);
            var result = VisitChildren(context);
            return new ResultNode(tagName, result?.InnerText ?? string.Empty);
        }

        public override ResultNode VisitHtmlChardata([NotNull] HTMLParser.HtmlChardataContext context)
        {
            var token = context?.HTML_TEXT()?.Payload as CommonToken;
            var value = token?.Text;
            return value == null ? null : new ResultNode("text", value);
        }

        private string GetTagName(HTMLParser.HtmlElementContext context)
        {
            var token = context?.TAG_NAME(0)?.Payload as CommonToken;
            var tagName = token?.Text?.ToLower().Trim();
            return tagName;
        }
    }
}
