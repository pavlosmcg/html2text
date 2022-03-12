using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Html2Text
{
    public class TextExtractionVisitor : HTMLParserBaseVisitor<string>
    {
        public new string AggregateResult(string aggregate, string nextResult)
        {
            if (string.IsNullOrWhiteSpace(aggregate))
                return nextResult;
            if (string.IsNullOrWhiteSpace(nextResult))
                return aggregate;
            return $"{aggregate} {nextResult}";
        }

        public override string VisitChildren(IRuleNode node)
        {
            string result = this.DefaultResult;
            int childCount = node.ChildCount;
            for (int i = 0; i < childCount && this.ShouldVisitNextChild(node, result); ++i)
            {
                string nextResult = node.GetChild(i).Accept<string>((IParseTreeVisitor<string>)this);
                result = AggregateResult(result, nextResult);
            }
            return result;
        }

        public override string VisitHtmlChardata([NotNull] HTMLParser.HtmlChardataContext context)
        {
            var token = context.HTML_TEXT().Payload as CommonToken;
            return token?.Text.Trim();
        }
    }
}
