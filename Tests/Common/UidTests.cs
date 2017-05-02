using AppLib.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common
{
    [TestClass]
    public class UidTests
    {
        [TestMethod]
        public void EqualsTest()
        {
            var id = UId.CreateFrom(0);
            var id2 = UId.CreateFrom(0);
            Assert.AreEqual(true, id == id2);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            var id = UId.CreateFrom(123);
            var id2 = UId.CreateFrom(0);
            Assert.AreEqual(true, id != id2);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var id = UId.CreateFrom(0xff);
            Assert.AreEqual("00-00-00-00-00-00-00-FF", id.ToString());
        }

        [TestMethod]
        public void CompareTests()
        {
            var id = UId.CreateFrom(0);
            var id2 = UId.CreateFrom(1);
            var id3 = UId.CreateFrom(0);
            Assert.AreEqual(-1, id.CompareTo(id2));
            Assert.AreEqual(0, id.CompareTo(id3));
            Assert.AreEqual(1, id2.CompareTo(id));
            Assert.AreEqual(true, id < id2);
            Assert.AreEqual(true, id2 > id);
        }
    }
}
