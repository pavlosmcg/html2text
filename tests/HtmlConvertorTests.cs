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
        [TestCase("<p>Some <span>text<span> after nested tag</p>", ExpectedResult = "Some text after nested tag")]
        [TestCase("<div><p>Inside elements nested correctly</p></div>", ExpectedResult = "Inside elements nested correctly")]
        [TestCase("<div><p>Inside elements nested incorrectly</div></p>", ExpectedResult = "Inside elements nested incorrectly")]
        [TestCase("<div><p>Inside elements nested incorrectly</div>", ExpectedResult = "Inside elements nested incorrectly")]
        public string GetText_ReturnsInnerText_WhenInputHasNestedTags(string input)
        {
            return Html.GetText(input);
        }

        [Test]
        public void GetText_ReturnsTextWithNewLines_WhenInputHasBr_Tag()
        {
            var input = "<p>Some more<br>Text on a new line</p>";
            var expected =
@"Some more
Text on a new line";

            Assert.AreEqual(expected, Html.GetText(input));
        }

        [Test]
        public void GetText_ReturnsMultipleLines_WhenInputContainsList()
        {
            var input = "<ul><li>First</li><li>Second</li></ul>";
            var expected = 
@"First
Second";
            Assert.AreEqual(expected, Html.GetText(input));
        }

        [TestCase("<span>Span text</span><p>Paragraph text</p>",
ExpectedResult = 
@"Span text
Paragraph text")]
        [TestCase("<span>Span text</span><p>Paragraph text</p><span>Another span</span>",
ExpectedResult = 
@"Span text
Paragraph text
Another span")]
        [TestCase("<span>Span text</span><p>Paragraph text</p><p>Another paragraph</p><span>Another span</span>",
ExpectedResult = 
@"Span text
Paragraph text
Another paragraph
Another span")]
        [TestCase("<span>Span text</span><p>Paragraph text</p><p>Another paragraph</p><span>Another span</span><span> with another span at the end</span>",
ExpectedResult =
@"Span text
Paragraph text
Another paragraph
Another span with another span at the end")]
        [TestCase("<span>Span text</span><p>Paragraph text</p><span>Span in the middle</span><p>Another paragraph</p>",
ExpectedResult =
@"Span text
Paragraph text
Span in the middle
Another paragraph")]
        public string GetText_ReturnsTextWithAppropriateNewLines_WhenInputHasMixedBlocksAndInlineElements(string input)
        {
            return Html.GetText(input);
        }

        [Test]
        public void GetText_ReturnsMultipleLines_WhenInputContainsTable()
        {
            var input = @"
<table>
  <thead>
    <tr>
      <th>Month</th>
      <th>Savings</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>January</td>
      <td>$100</td>
    </tr>
    <tr>
      <td>February</td>
      <td>$80</td>
    </tr>
  </tbody>
  <tfoot>
    <tr>
      <td>Sum</td>
      <td>$180</td>
    </tr>
  </tfoot>
</table>";
            var expected = 
@"Month Savings
January $100
February $80
Sum $180";
            Assert.AreEqual(expected, Html.GetText(input));
        }

    }
}