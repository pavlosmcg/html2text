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
            return visitor.Visit(documentContext).ToString().Trim();
        }
    }
}
