using System.IO;
using Html2Text;

namespace Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var fileStream = new FileStream("clampdown.html", FileMode.Open, FileAccess.Read))
            {
                var result = Html.GetText(fileStream);
                System.Console.WriteLine(result);
            }
        }
    }
}
