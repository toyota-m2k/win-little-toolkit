using io.github.toyota32k.toolkit.net;
using System.Collections;

namespace UnitTest
{
    [TestClass]
    public sealed class UtilsTest
    {
        [TestMethod]
        public void GetValueTest() {
            var dict = new Dictionary<string, string> {
                {"key1", "value1"},
                {"key2", "value2"},
            };
            Assert.AreEqual("value1", dict.GetValue("key1"));
            Assert.AreEqual("value2", dict.GetValue("key2"));

            var wr = new WeakReference<string>("value");
            Assert.AreEqual("value", wr.GetValue());
        }

        [TestMethod]
        public void EmptyTest() {
            List<string>? list = null;
            Assert.IsTrue(Utils.IsNullOrEmpty(list));
            Assert.IsTrue(list.IsEmpty());

            list = new List<string>();
            Assert.IsTrue(Utils.IsNullOrEmpty(list));
            Assert.IsTrue(list.IsEmpty());

            list = new List<string> { "a", "b", "c" };
            Assert.IsFalse(Utils.IsNullOrEmpty(list));
            Assert.IsFalse(list.IsEmpty());

            string? str = null;
            Assert.IsTrue(str.IsEmpty());
            str = "";
            Assert.IsTrue(str.IsEmpty());
            str = "abc";
            Assert.IsFalse(str.IsEmpty());
        }

        [TestMethod]
        public void ApplyTest() {
            var list = (new List<int> { 1, 2, 3 }).Apply(l => {
                l.Add(4);
            });
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(4, list[3]);

            var count = list.Run(l => l.Count);
            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void ToEnumerableTest() {
            IList list = new List<int> { 1, 2, 3 };
            var e = list.ToEnumerable<int>();
            Assert.AreEqual(1, e.First());
            Assert.AreEqual(2, e.Skip(1).First());
            Assert.AreEqual(3, e.Skip(2).First());
        }

        [TestMethod]
        public void ToSingleEnumerableTest() {
            var e = Utils.ToSingleEnumerable(123);
            Assert.AreEqual(123, e.First());
        }
        [TestMethod]
        public void ArrayOfTest() {
            var a = Utils.ArrayOf(1, 2, 3);
            Assert.AreEqual(3, a.Length);
            Assert.AreEqual(1, a[0]);
            Assert.AreEqual(2, a[1]);
            Assert.AreEqual(3, a[2]);
        }

        [TestMethod]
        public void ContainsIgnoreCaseTest() {
            Assert.IsTrue("abc".ContainsIgnoreCase("a"));
            Assert.IsTrue("abc".ContainsIgnoreCase("A"));
            Assert.IsTrue("abc".ContainsIgnoreCase("b"));
            Assert.IsTrue("abc".ContainsIgnoreCase("B"));
            Assert.IsTrue("abc".ContainsIgnoreCase("c"));
            Assert.IsTrue("abc".ContainsIgnoreCase("C"));
            Assert.IsFalse("abc".ContainsIgnoreCase("d"));
            Assert.IsFalse("abc".ContainsIgnoreCase("D"));
        }
    }
}
