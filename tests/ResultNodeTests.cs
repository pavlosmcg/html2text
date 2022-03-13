using System;
using NUnit.Framework;

namespace Html2Text.Tests
{
    public class ResultNodeTests
    {
        [Test]
        public void Append_SeparatesWithOneNewLine_WhenBothElementsAreBothBlocks()
        {
            var current = new ResultNode("div", "current");
            var nextNode = new ResultNode("div", "next");

            var result = current.Append(nextNode);

            Assert.AreEqual($"current{Environment.NewLine}next", result.InnerText);
        }

        [Test]
        public void Append_SeparatesWithOneNewLine_WhenBlockElementIsFollowedByInline()
        {
            var current = new ResultNode("div", "current");
            var nextNode = new ResultNode("span", "next");

            var result = current.Append(nextNode);

            Assert.AreEqual($"current{Environment.NewLine}next", result.InnerText);
        }

        [Test]
        public void Append_SeparatesWithOneNewLine_WhenInlineElementIsFollowedByBlock()
        {
            var current = new ResultNode("span", "current");
            var nextNode = new ResultNode("div", "next");

            var result = current.Append(nextNode);

            Assert.AreEqual($"current{Environment.NewLine}next", result.InnerText);
        }

        [Test]
        public void Append_ConcatenatesNodes_WhenBothElementsAreInline()
        {
            var current = new ResultNode("span", "current");
            var nextNode = new ResultNode("span", "next");

            var result = current.Append(nextNode);

            Assert.AreEqual($"currentnext", result.InnerText);
        }

        [Test]
        public void Append_SeparatesWithOneSpace_WhenCurrentAndNextAreBothTableRows()
        {
            var current = new ResultNode("td", "current");
            var nextNode = new ResultNode("td", "next");

            var result = current.Append(nextNode);

            Assert.AreEqual($"current next", result.InnerText);
        }

        [Test]
        public void Append_DoesNotSeparateWithOneSpace_WhenCurrentAndNextAreNotBothTableRows()
        {
            var current = new ResultNode("td", "current");
            var nextNode = new ResultNode("span", "next");

            var result = current.Append(nextNode);

            Assert.AreEqual($"currentnext", result.InnerText);
        }
    }
}