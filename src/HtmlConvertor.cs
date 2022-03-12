using System;
using System.Text;
using Antlr4.Runtime;

namespace Html2Text
{
    public static class Html
    {
        public static string GetText(string html)
        {
            var inputStream = new AntlrInputStream(html);
            var lexer = new HTMLLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new HTMLParser(tokenStream);
            var documentContext = parser.htmlDocument();

            var visitor = new TextExtractionVisitor();
            visitor.Visit(documentContext);

            var builder = new StringBuilder();
            foreach (var fragment in visitor.TextFragments)
            {
                builder.Append(fragment); // TODO append with new line
            }

            return builder.ToString();
        }
    }
}
