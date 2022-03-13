using System;

namespace Html2Text
{
    public class ResultNode
    {
        public string NodeType { get; }
        public string InnerText { get; }

        public ResultNode(string nodeType, string innerText)
        {
            NodeType = nodeType;
            InnerText = innerText;
        }

        public ResultNode Append(ResultNode next)
        {
            if (next.IsBreakElement())
            {
                return new ResultNode(next.NodeType, $"{InnerText}{Environment.NewLine}");
            }

            if (string.IsNullOrWhiteSpace(InnerText))
            {
                return next;
            }

            // either current or next element is a block
            if (IsBlockElement() || next.IsBlockElement())
            {
                return new ResultNode(next.NodeType, $"{InnerText}{Environment.NewLine}{next.InnerText}");
            }

            // items in table rows should be kept on the same line
            if (IsTableRowItem() && next.IsTableRowItem())
            {
                return new ResultNode(next.NodeType, $"{InnerText} {next.InnerText}");
            }

            // inline element followed by more another inline
            return new ResultNode(next.NodeType, $"{InnerText}{next.InnerText}");
        }

        public bool IsTableRowItem()
        {
            return NodeType switch
            {
                "th" => true,
                "td" => true,
                _ => false
            };
        }

        public bool IsBreakElement()
        {
            return NodeType switch
            {
                "br" => true,
                "hr" => true,
                _ => false
            };
        }

        public bool IsBlockElement()
        {
            return NodeType switch
            {
                "address" => true,
                "article" => true,
                "aside" => true,
                "blockquote" => true,
                "canvas" => true,
                "details" => true,
                "dialog" => true,
                "dd" => true,
                "div" => true,
                "dl" => true,
                "dt" => true,
                "fieldset" => true,
                "figcaption" => true,
                "figure" => true,
                "footer" => true,
                "form" => true,
                "h1" => true,
                "h2" => true,
                "h3" => true,
                "h4" => true,
                "h5" => true,
                "h6" => true,
                "header" => true,
                "hgroup" => true,
                "li" => true,
                "main" => true,
                "nav" => true,
                "noscript" => true,
                "ol" => true,
                "p" => true,
                "pre" => true,
                "section" => true,
                "table" => true,
                "thead" => true,
                "tbody" => true,
                "tfoot" => true,
                "tr" => true,
                "ul" => true,
                "video" => true,
                _ => false
            };
        }
    }
}