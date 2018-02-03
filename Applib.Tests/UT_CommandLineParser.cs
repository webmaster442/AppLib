using AppLib.Common.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppLib.Tests
{
    [TestClass]
    public class UT_CommandLineParser
    {
        [TestMethod]
        [DataRow("asd.exe test.file", 2, 0, 0)]
        [DataRow("asd.exe /switch test.file", 1, 0, 1)]
        [DataRow("asd.exe /standalone1 /switch test.file /standalone2", 1, 2, 1)]
        public void Assert_CommandLineParser_SplitsArguments_AsExpected(string rawCommand, int FilesCount, int switchCount, int switchWithArgumentCount)
        {
            ParameterParser parser = new ParameterParser(rawCommand, false, true);
            Assert.AreEqual(FilesCount, parser.Files.Count);
            Assert.AreEqual(switchCount, parser.StandaloneSwitches.Count);
            Assert.AreEqual(switchWithArgumentCount, parser.SwitchesWithValue.Count);
        }

        [TestMethod]
        [DataRow("asd.exe test.file", 1, 0, 0)]
        [DataRow("asd.exe /switch test.file", 0, 0, 1)]
        [DataRow("asd.exe /standalone1 /switch test.file /standalone2", 0, 2, 1)]
        public void Assert_CommandLineParser_SkipsProgramName(string rawCommand, int FilesCount, int switchCount, int switchWithArgumentCount)
        {
            ParameterParser parser = new ParameterParser(rawCommand, true, true);
            Assert.AreEqual(false, parser.Files.Contains("asd.exe"));
            Assert.AreEqual(FilesCount, parser.Files.Count);
            Assert.AreEqual(switchCount, parser.StandaloneSwitches.Count);
            Assert.AreEqual(switchWithArgumentCount, parser.SwitchesWithValue.Count);
        }

        [TestMethod]
        [DataRow("asd.exe \"escaped.file\" \"c:\\test folder\test.file\"")]
        public void Assert_CommandLineParser_RemovesQoutesFromFiles(string rawCommand)
        {
            ParameterParser parser = new ParameterParser(rawCommand, false, true);
            Assert.AreEqual(3, parser.Files.Count);
            Assert.AreEqual("escaped.file", parser.Files[1]);
            Assert.AreEqual("c:\\test folder\test.file", parser.Files[2]);
        }
    }
}
