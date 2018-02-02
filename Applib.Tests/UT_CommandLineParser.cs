using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLib.Common.Console;

namespace AppLib.Tests
{
    [TestClass]
    public class UT_CommandLineParser
    {
        [TestMethod]
        [DataRow("asd.exe test.file", 1, 0, 0)]
        public void CommandLineParserTests(string rawCommand, int FilesCount, int switchCount, int switchWithArgumentCount)
        {
            Command
        }
    }
}
