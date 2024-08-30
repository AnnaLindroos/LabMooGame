using LabMooGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGameTests1;

[TestClass]
public class ConsoleIOTests
{
    [TestMethod]
    public void ReadTest()
    {
        var input = "test input";
        var consoleIO = new ConsoleIO();

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            var result = consoleIO.Read();

            Assert.AreEqual(input, result);
        }
    }

    [TestMethod]
    public void WriteLineTest()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        var consoleIO = new ConsoleIO();
        var message = "Hello, World!";

        consoleIO.WriteLine(message);

        Assert.AreEqual($"{message}{Environment.NewLine}", output.ToString());
    }
}
