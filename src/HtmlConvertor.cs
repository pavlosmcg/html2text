using System.IO;
using Antlr4.Runtime;
using Html2Text.Antlr;

namespace Html2Text
{
    public static class Html
    {
        public static string GetText(string html)
        {
            var inputStream = new AntlrInputStream(html);
            return GetDocumentText(inputStream);
        }

        public static string GetText(Stream htmlStream)
        {
            var inputStream = new AntlrInputStream(htmlStream);
            return GetDocumentText(inputStream);
        }

        private static string GetDocumentText(AntlrInputStream inputStream)
        {
            var lexer = new HTMLLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new HTMLParser(tokenStream) { BuildParseTree = true };
            var documentContext = parser.htmlDocument();

            var visitor = new TextExtractionVisitor();
            return visitor.Visit(documentContext).InnerText;
        }
    }
}
