using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppLib.Common.Console;

namespace AppLib.Tests
{
    [TestClass]
    public class UT_CommandLineParser
    {
        [TestMethod]
        [DataRow("asd.exe test.file", 2, 0, 0)]
        [DataRow("asd.exe /switch test.file", 1, 0, 1)]
        [DataRow("asd.exe /standalone1 /switch test.file /standalone2", 1, 2, 1)]
        public void CommandLineParserTests(string rawCommand, int FilesCount, int switchCount, int switchWithArgumentCount)
        {
            ParameterParser parser = new ParameterParser(rawCommand, true);
            Assert.AreEqual(FilesCount, parser.Files.Count);
            Assert.AreEqual(switchCount, parser.StandaloneSwitches.Count);
            Assert.AreEqual(switchWithArgumentCount, parser.SwitchesWithValue.Count);
        }
    }
}
