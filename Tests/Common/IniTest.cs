using AppLib.Common.INI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common
{
    [TestClass]
    public class IniTest
    {
        private readonly IniFile ini;

        public IniTest()
        {
            ini = new IniFile();
            ini.ReadFromString(Tests.Properties.Resources.IniTest);
        }

        [TestMethod]
        public void SectionTest()
        {
            Assert.AreEqual("Section", ini.Categories[0]);
            Assert.AreEqual("OtherSection", ini.Categories[1]);
        }

        [TestMethod]
        public void MiscTest()
        {
            Assert.AreEqual(true, ini.ContainsSetting("Section", "DoubleTest"));
            Assert.AreEqual(false, ini.ContainsSetting("Section", "DoesNotExist"));
        }

        [TestMethod]
        public void KeyTests()
        {
            ini.CurrentCategory = "Section";
            Assert.AreEqual("Test string", ini.GetSetting<string>("StringTest"));
            Assert.AreEqual(3893, ini.GetSetting<int>("IntTest"));
            Assert.AreEqual(5366.5699d, ini.GetSetting<double>("DoubleTest"));
        }

        [TestMethod]
        public void MalformedTests()
        {
            ini.CurrentCategory = "OtherSection";
            Assert.AreEqual("Blah", ini.GetSetting<string>("MalFomated1"));
            Assert.AreEqual("Blah33", ini.GetSetting<string>("MalFormated22"));
        }

        [TestMethod]
        public void SaveTest()
        {
            var save = new IniFile();
            save.CurrentCategory = "SaveTest";
            save.SetSetting<int>("IntTest", 42);
            save.SetSetting<double>("DobuleTest", 33.222d);
            save.SetSetting<string>("StringTest", "Works");

            var loaded = new IniFile();
            loaded.ReadFromString(save.SaveToString());
            loaded.CurrentCategory = "SaveTest";
            Assert.AreEqual(42, loaded.GetSetting<int>("IntTest"));
            Assert.AreEqual(33.222d, loaded.GetSetting<double>("DobuleTest"));
            Assert.AreEqual("Works", loaded.GetSetting<string>("StringTest"));
        }
    }
}
