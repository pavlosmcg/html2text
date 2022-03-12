using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Html2Text
{
    public class TextExtractionVisitor : HTMLParserBaseVisitor<string> // TODO can we use a better generic type here?
    {
        public List<string> TextFragments = new List<string>();

        public override string VisitHtmlChardata([NotNull] HTMLParser.HtmlChardataContext context)
        {
            ITerminalNode text = context.HTML_TEXT();
            TextFragments.Add(text.ToString());

            return VisitChildren(context); // TODO - do we visit the children or quit if it's just text?
        }
    }
}
