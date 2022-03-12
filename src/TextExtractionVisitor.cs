using System;
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

        public override StringBuilder VisitHtmlElements([NotNull] HTMLParser.HtmlElementsContext context)
        {
            // todo see if there are siblings here that need joining with spaces
            //var childElement = context?.children[0] as HTMLParser.HtmlElementContext;

            return VisitChildren(context);
        }

        public override StringBuilder VisitHtmlContent([NotNull] HTMLParser.HtmlContentContext context)
        {
            var result = VisitChildren(context);
            if (result != null)
            {
                result = new StringBuilder(result.ToString().Trim(' '));
            }

            return result;
        }

        public override StringBuilder VisitHtmlElement([NotNull] HTMLParser.HtmlElementContext context)
        {
            var result = VisitChildren(context);
            if (NeedsSeparator(context, out string separator))
            {
                result ??= new StringBuilder();
                result.Append(separator);
            }

            return result;
        }

        public override StringBuilder VisitHtmlChardata([NotNull] HTMLParser.HtmlChardataContext context)
        {
            var token = context?.HTML_TEXT()?.Payload as CommonToken;
            var value = token?.Text;
            return string.IsNullOrWhiteSpace(value) ? null : new StringBuilder(value);
        }

        private string GetTagName(HTMLParser.HtmlElementContext context)
        {
            var token = context?.TAG_NAME(0)?.Payload as CommonToken;
            var tagName = token?.Text?.Trim();
            return tagName;
        }

        private bool NeedsSeparator(HTMLParser.HtmlElementContext context, out string separator)
        {
            separator = Environment.NewLine;
            switch (GetTagName(context))
            {
                case "li":
                case "p":
                case "br":
                case "tr":
                case "dt":
                case "dd":
                    return true;
                case "th":
                case "td":
                    separator = " ";
                    return true;
                default:
                    return false;
            }
        }

        private bool NeedsSpaceBetweenSiblings(HTMLParser.HtmlElementContext context)
        {
            return GetTagName(context) switch
            {
                "th" => true,
                "dt" => true,
                _ => false
            };
        }

    }
}
