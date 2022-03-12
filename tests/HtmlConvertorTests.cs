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
        [TestCase("<em>blorgfester</em>")]
        [TestCase("<blockquote>blorgfester</blockquote>")]
        [TestCase("<code>blorgfester</code>")]
        [TestCase("<samp>blorgfester</samp>")]
        [TestCase("<kbd>blorgfester</kbd>")]
        [TestCase("<var>blorgfester</var>")]
        [TestCase("<imadethisup>blorgfester</imadethisup>")]
        public void GetText_ReturnsInnerText_WhenInputIsASingleTag(string input)
        {
            var result = Html.GetText(input);
            Assert.AreEqual("blorgfester", result);
        }

        [TestCase("<p>Some <span>text<span></p>", ExpectedResult="Some text")]
        [TestCase("<p attr='something'>Some <br>text on a new line<span></p>", ExpectedResult="Some text on a new line")]
        [TestCase("<ul><li>List item text</li></ul>", ExpectedResult="List item text")]
        [TestCase("<ul><li>First</li><li>Second</li></ul>", ExpectedResult="First Second")]
        public string GetText_ReturnsInnerText_WhenInputHasNestedTags(string input)
        {
            return Html.GetText(input);
        }
    }
}