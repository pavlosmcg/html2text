using System;
using NUnit.Framework;

namespace Html2Text.Tests
{
    public class HtmlConvertorTests
    {
        [TestCase("<span>blorgfester</span>")]
        [TestCase("<div>blorgfester</div>")]
        [TestCase("<p>blorgfester</p>")]
        [TestCase("<h1>blorgfester</h1>")]
        [TestCase("<strong>blorgfester</strong>")]
        [TestCase("<imadethisup>blorgfester</imadethisup>")]
        public void GetText_ReturnsInnerText_WhenInputIsASingleTag(string input)
        {
            var result = Html.GetText(input);
            Assert.AreEqual("blorgfester", result);
        }

        [TestCase("<ul><li>List item text</li></ul>", ExpectedResult = "List item text")]
        [TestCase("<dd><i>What are we going to do now?</i></dd>", ExpectedResult = "What are we going to do now?")]
        [TestCase("<p>Some <span>text<span></p>", ExpectedResult="Some text")]
        public string GetText_ReturnsInnerText_WhenInputHasNestedTags(string input)
        {
            return Html.GetText(input);
        }

        [Test]
        public void GetText_ReturnsTextWithNewLines_WhenInputHasBr_Tag()
        {
            var input = "<p attr='something'>Some more<br>Text on a new line<span></p>";
            var expected = @"
Some more
Text on a new line";

            Assert.AreEqual(expected.TrimStart(), Html.GetText(input));
        }

        [Test]
        public void GetText_ReturnsMultipleLines_WhenInputIsList()
        {
            var input = "<ul><li>First</li><li>Second</li></ul>";
            var expected = @"
First
Second";
            Assert.AreEqual(expected.TrimStart(), Html.GetText(input));
        }

    }
}