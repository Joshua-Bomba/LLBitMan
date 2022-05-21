using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLBitMan;
using System.Threading;

namespace LLBitManTester
{
    [TestFixture]
    public class ContextTest
    {


        private class TestSession : ISession//copypasta from https://github.dev/dotnet/aspnetcore/tree/02c6de4ba6022025fcda7581415f310f8c73cdc3
        {
            private Dictionary<string, byte[]> _innerDictionary = new Dictionary<string, byte[]>();

            public IEnumerable<string> Keys { get { return _innerDictionary.Keys; } }

            public string Id => "TestId";

            public bool IsAvailable { get; } = true;

            public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult(0);
            }

            public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult(0);
            }

            public void Clear()
            {
                _innerDictionary.Clear();
            }

            public void Remove(string key)
            {
                //removing a key that does not exist is okay on a real session
                _innerDictionary.Remove(key);
            }

            public void Set(string key, byte[] value)
            {
                //I Check the DistributedSession https://github.dev/dotnet/aspnetcore/tree/02c6de4ba6022025fcda7581415f310f8c73cdc3
                //and it appaers that it will throw an exception if this is set to null
                _innerDictionary[key] = value.ToArray();
            }

            public bool TryGetValue(string key, out byte[] value)
            {
                return _innerDictionary.TryGetValue(key, out value);
            }
        }


        public class BitMoreComplicatedObject
        {
            public string AProp { get; set; }

            public object RefObject { get; set; }
        }

        [Test]
        public void BasicTest()
        {
            TestSession session = new TestSession();
            session.SetObject<int?>("nulltest", null);
            int? a = session.GetObject<int?>("nulltest");

            session.SetObject<object>("nulltest2", null);
            object? nt2 = session.GetObject<object>("nulltest2");

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

            session.SetObject("object", bmco);

            //This will not be the same object
            BitMoreComplicatedObject result = session.GetObject<BitMoreComplicatedObject>("object");

            TestPrimativeValue(session, 1);
        }

        static void TestPrimativeValue(ISession session, ulong index)
        {
            if (index <= 1)
            {
                bool bl = index != 0;
                session.SetObject("bool", bl);
                Assert.AreEqual(bl, session.GetObject<bool>("bool"));
            }

            if (index <= byte.MaxValue)
            {
                Guid g = Guid.NewGuid();
                byte b = (byte)index;
                sbyte sb = (sbyte)index;

               session.SetObject("guid", g);
               session.SetObject("byte", b);
                session.SetObject("sbyte", sb);
                Assert.AreEqual(g, session.GetObject<Guid>("guid"));
                Assert.AreEqual(b, session.GetObject<byte>("byte"));
                Assert.AreEqual(sb, session.GetObject<sbyte>("sbyte"));
            }
            if (index <= ushort.MaxValue)
            {
                short s = (short)index;
                ushort us = (ushort)index;
                char c = (char)index;
                session.SetObject("short", s);
                session.SetObject("ushort", us);
                session.SetObject("char", c);

                Assert.AreEqual(us, session.GetObject<short>("short"));
                Assert.AreEqual(s, session.GetObject<ushort>("ushort"));
                Assert.AreEqual(c, session.GetObject<char>("char"));
            }
            if (index <= uint.MaxValue)
            {
                float f = (float)index;
                int i = (int)index;
                uint ui = (uint)index;
                session.SetObject("float", f);
                session.SetObject("int", i);
                session.SetObject("uint", ui);

                Assert.AreEqual(f, session.GetObject<float>("float"));
                Assert.AreEqual(i, session.GetObject<int>("int"));
                Assert.AreEqual(ui, session.GetObject<uint>("uint"));
            }

            if (index <= ulong.MaxValue)
            {
                decimal d = (decimal)index;
                long l = (long)index;
                ulong ul = (ulong)index;
                double dl = (double)index;

                session.SetObject("decimal", d);
                session.SetObject("long", l);
                session.SetObject("ulong", ul);
                session.SetObject("double", dl);

                Assert.AreEqual(d, session.GetObject<decimal>("decimal"));
                Assert.AreEqual(l, session.GetObject<long>("long"));
                Assert.AreEqual(ul,session.GetObject<ulong>("ulong"));
                Assert.AreEqual(dl, session.GetObject<double>("double"));
            }
        }

    }
}
