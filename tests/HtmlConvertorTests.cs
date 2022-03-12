using Html2Text;
using NUnit.Framework;

namespace Html2Text.Tests
{
    public class HtmlConvertorTests
    {
        [Test]
        public void GetText_ReturnsInnerText_WhenDocumentIsASimpleTag()
        {
            var result = Html.GetText("<span>blorgfester</span>");

            Assert.AreEqual("blorgfester", result);
        }
    }
}