using Html2Text;
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
        public void GetText_ReturnsInnerText_WhenDocumentIsASingleTag(string input)
        {
            var result = Html.GetText(input);
            Assert.AreEqual("blorgfester", result);
        }
    }
}