﻿using LLDataMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LLBitManTester
{
    class Program
    {
        private const uint THREAD_SPAWN = 1;
        private const long END = 1000;


        class Assert
        {
            public static void IsTrue(bool b)
            {
                if(!b)
                {
                    throw new Exception("Expected True but recieved false");
                }
            }

            public static void AreEqual(object expected, object actual)
            {
                if(!expected.Equals(actual))
                {
                    throw new Exception($"{expected} Does Not Equal {actual}");
                }
            }
        }

        static void TestValue(ulong index)
        {
            if (index <= 1)
            {
                bool bl = index != 0;
                byte[] bla = bl.ToByteArray();

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(bla, out bool rbl));
                Assert.AreEqual(bl, rbl);
            }

            if (index <= byte.MaxValue)
            {
                Guid g = Guid.NewGuid();
                byte b = (byte)index;
                sbyte sb = (sbyte)index;

                byte[] ba = b.ToByteArray();
                byte[] sba = sb.ToByteArray();
                byte[] ga = g.ToByteArray();

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ga, out Guid rg));
                Assert.AreEqual(g, rg);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ba, out byte rb));
                Assert.AreEqual(b, rb);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(sba, out sbyte rsb));
                Assert.AreEqual(sb, rsb);

            }
            if (index <= ushort.MaxValue)
            {
                short s = (short)index;
                ushort us = (ushort)index;
                char c = (char)index;
                byte[] sa = s.ToByteArray();
                byte[] usa = us.ToByteArray();
                byte[] ca = c.ToByteArray();

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(sa, out short ra));
                Assert.AreEqual(s, ra);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(usa, out ushort rus));
                Assert.AreEqual(us, rus);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ca, out char rc));
                Assert.AreEqual(c, rc);
            }
            if (index <= uint.MaxValue)
            {
                float f = (float)index;
                int i = (int)index;
                uint ui = (uint)index;
                byte[] fa = f.ToByteArray();
                byte[] ia = i.ToByteArray();
                byte[] uia = ui.ToByteArray();

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(fa, out float rf));
                Assert.AreEqual(f, rf);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ia, out int ri));
                Assert.AreEqual(i, ri);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(uia, out uint rui));
                Assert.AreEqual(ui, rui);
            }

            if (index <= ulong.MaxValue)
            {

                decimal d = (decimal)index;
                long l = (long)index;
                ulong ul = (ulong)index;
                double dl = (double)index;


                byte[] da = d.ToByteArray();
                byte[] la = l.ToByteArray();
                byte[] ula = ul.ToByteArray();
                byte[] dla = dl.ToByteArray();

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(da, out decimal rd));
                Assert.AreEqual(d, rd);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(la, out long rl));
                Assert.AreEqual(l, rl);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ula, out ulong rul));
                Assert.AreEqual(ul, rul);

                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(dla, out double rdl));
                Assert.AreEqual(dl, rdl);
            }
        }

        static void ThreadProcesses(int start)
        {
            for (ulong i = (ulong)start; i <= END; i += THREAD_SPAWN)
            {
                TestValue(i);
            }
        }

        static void Main(string[] args)
        {

            Thread[] threads = new Thread[THREAD_SPAWN];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(() => ThreadProcesses(i)));
            }

            for(int i = 0;i < threads.Length;i++)
            {
                threads[i].Start();
            }

            for(int i = 0;i < threads.Length;i++)
            {
                threads[i].Join();
            }


        }
    }
}
