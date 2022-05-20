using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLBitMan;

namespace LLBitManTester
{
    [TestFixture]
    public class ContextTest
    {

        public class BitMoreComplicatedObject
        {
            public string AProp { get; set; }

            public object RefObject { get; set; }
        }

        [Test]
        public void BasicTest()
        {
            DefaultHttpContext context = new DefaultHttpContext();
            context.Session.SetObject<int?>("nulltest", null);
            int? a = context.Session.GetObject<int?>("nulltest");

            int refObj = 14;
            BitMoreComplicatedObject l1 = new BitMoreComplicatedObject
            {
                AProp = "Nested Prop",
                RefObject = refObj
            };

            BitMoreComplicatedObject bmco = new BitMoreComplicatedObject
            {
                AProp = "Test Prop",
                RefObject = l1
            };

            context.Session.SetObject("object", bmco);

            //This will not be the same object
            BitMoreComplicatedObject result = context.Session.GetObject<BitMoreComplicatedObject>("object");

            TestPrimativeValue(context, 1);
        }

        static void TestPrimativeValue(DefaultHttpContext context, ulong index)
        {
            if (index <= 1)
            {
                bool bl = index != 0;
                context.Session.SetObject("bool", bl);
                Assert.AreEqual(bl, context.Session.GetObject<bool>("bool"));
            }

            if (index <= byte.MaxValue)
            {
                Guid g = Guid.NewGuid();
                byte b = (byte)index;
                sbyte sb = (sbyte)index;

                context.Session.SetObject("guid", g);
                context.Session.SetObject("byte", b);
                context.Session.SetObject("sbyte", sb);
                Assert.AreEqual(b, context.Session.GetObject<Guid>("guid"));
                Assert.AreEqual(g, context.Session.GetObject<byte>("byte"));
                Assert.AreEqual(sb, context.Session.GetObject<sbyte>("sbyte"));
            }
            if (index <= ushort.MaxValue)
            {
                short s = (short)index;
                ushort us = (ushort)index;
                char c = (char)index;
                context.Session.SetObject("short", s);
                context.Session.SetObject("ushort", us);
                context.Session.SetObject("char", c);

                Assert.AreEqual(us, context.Session.GetObject<short>("short"));
                Assert.AreEqual(s, context.Session.GetObject<ushort>("ushort"));
                Assert.AreEqual(c, context.Session.GetObject<char>("char"));
            }
            if (index <= uint.MaxValue)
            {
                float f = (float)index;
                int i = (int)index;
                uint ui = (uint)index;
                context.Session.SetObject("float", f);
                context.Session.SetObject("int", i);
                context.Session.SetObject("uint", ui);

                Assert.AreEqual(f, context.Session.GetObject<float>("float"));
                Assert.AreEqual(i, context.Session.GetObject<int>("int"));
                Assert.AreEqual(ui, context.Session.GetObject<uint>("uint"));
            }

            if (index <= ulong.MaxValue)
            {
                decimal d = (decimal)index;
                long l = (long)index;
                ulong ul = (ulong)index;
                double dl = (double)index;

                context.Session.SetObject("decimal", d);
                context.Session.SetObject("long", l);
                context.Session.SetObject("ulong", ul);
                context.Session.SetObject("double", dl);

                Assert.AreEqual(d, context.Session.GetObject<decimal>("decimal"));
                Assert.AreEqual(l, context.Session.GetObject<long>("long"));
                Assert.AreEqual(ul, context.Session.GetObject<ulong>("ulong"));
                Assert.AreEqual(dl, context.Session.GetObject<double>("double"));
            }
        }

    }
}
