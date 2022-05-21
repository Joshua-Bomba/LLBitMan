using LLBitMan;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LLBitManTester
{
    [TestFixture]
    public class LLBitManTester
    {
        private static readonly uint THREAD_SPAWN_DEFAULT = (uint)Environment.ProcessorCount;
        private static uint INCREMENT = THREAD_SPAWN_DEFAULT;
        private const ulong INTERVAL = 100000000;
        private const ulong UINT_END = uint.MaxValue;

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
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(bla, out bool rbl));
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
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(ga, out Guid rg));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(ba, out byte rb));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(sba, out sbyte rsb));
                Assert.AreEqual(b, rb);
                Assert.AreEqual(g, rg);
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

                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(sa, out short ra));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(usa, out ushort rus));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(ca, out char rc));
                Assert.AreEqual(us, rus);
                Assert.AreEqual(s, ra);
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

                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(fa, out float rf));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(ia, out int ri));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(uia, out uint rui));
                Assert.AreEqual(f, rf);
                Assert.AreEqual(i, ri);
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
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(da, out decimal rd));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(la, out long rl));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(ula, out ulong rul));
                Assert.IsTrue(LLBitMan.LLByteArrayManager.TryGetPrimitiveValue(dla, out double rdl));
                Assert.AreEqual(d, rd);
                Assert.AreEqual(l, rl);
                Assert.AreEqual(ul, rul);
                Assert.AreEqual(dl, rdl);
            }
        }

        static void PastUIntMax(Action<ulong> f)
        {
            ulong endPoint = ulong.MaxValue;
            ulong i = uint.MaxValue;
            while (i < endPoint)
            {
                i = (i << 4);
                for (byte k = 0; k < 15; k++)
                {
                    f(i);
                    i += 1;
                }
                f(i);
            }
        }

        static void ThreadProcessesTimer()
        {
            Stopwatch stopWatch = new Stopwatch();
            ulong i = 0;
            ulong endTimerPoint = INTERVAL;
            ulong endPoint = UINT_END;
            Console.WriteLine("starting point of " + i + " with increment of " + INCREMENT + " Endpoint is " + endPoint);
            while (i <= endPoint)
            {
                stopWatch.Start();
                while (i <= endTimerPoint)
                {
                    TestValue(i);
                    i += INCREMENT;
                }

                endTimerPoint += INTERVAL;
                if (endTimerPoint > endPoint)
                {
                    endTimerPoint = endPoint;
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine("RunTime Till INTERVAL of " + i + " took " + ts.TotalMilliseconds + "ms");
                stopWatch.Reset();
            }

        }

        static void ThreadProcesses(ulong start)
        {
            ulong i = 0;
            uint increment = INCREMENT;
            ulong endPoint = UINT_END;
            while (i <= endPoint)
            {
                TestValue(i);
                i += increment;
            }
        }


        public static void AllPossibleScenerios(Action<ulong> f)
        {
            uint cores = THREAD_SPAWN_DEFAULT;
            Console.WriteLine("Threads Creating: " + cores);
            Thread[] threads = new Thread[cores];
            for (int i = 0; i < threads.Length; i++)
            {
                if (i == 0)
                {
                    threads[i] = new Thread(new ThreadStart(ThreadProcessesTimer));
                }
                else
                {
                    ulong index = (ulong)i;
                    threads[i] = new Thread(new ThreadStart(() => ThreadProcesses(index)));
                }

            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            PastUIntMax(f);

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Full Run Time " + elapsedTime);
            stopWatch.Stop();
        }


        [Test]
        public void StandardAllPossibleScenerion()
        {
            AllPossibleScenerios(TestValue);
        }
    }
}
