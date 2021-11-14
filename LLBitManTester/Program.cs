//#define GENERIC_TEST
using LLDataMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LLBitManTester
{
    class Program
    {
        private const uint THREAD_SPAWN_DEFAULT = 8;
        private static uint FIRST_INCREMENT = THREAD_SPAWN_DEFAULT;
        private static uint SECOND_INCREMENT = THREAD_SPAWN_DEFAULT * 5;
        private const ulong INTERVAL = 100000000;
        private const ulong UINT_END = uint.MaxValue;
        private static ulong ULONG_END_MARGIN = ulong.MaxValue - SECOND_INCREMENT;

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
#if GENERIC_TEST
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(bla, out bool rbl));
#else
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(bla, out bool rbl));
#endif
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
#if GENERIC_TEST
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ga, out Guid rg));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ba, out byte rb));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(sba, out sbyte rsb));
#else
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(ga, out Guid rg));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(ba, out byte rb));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(sba, out sbyte rsb));
#endif
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

#if GENERIC_TEST
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(sa, out short ra));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(usa, out ushort rus));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ca, out char rc));
#else
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(sa, out short ra));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(usa, out ushort rus));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(ca, out char rc));
#endif
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

#if GENERIC_TEST
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(fa, out float rf));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ia, out int ri));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(uia, out uint rui));
#else
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(fa, out float rf));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(ia, out int ri));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(uia, out uint rui));
#endif
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
#if GENERIC_TEST
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(da, out decimal rd));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(la, out long rl));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(ula, out ulong rul));
                Assert.IsTrue(LLDataMan.LLBitMan.TryGetPrimativeValue(dla, out double rdl));
#else
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(da, out decimal rd));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(la, out long rl));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(ula, out ulong rul));
                Assert.IsTrue(LLDataMan.LLBitMan.TryReinterpretCast(dla, out double rdl));
#endif
                Assert.AreEqual(d, rd);
                Assert.AreEqual(l, rl);
                Assert.AreEqual(ul, rul);
                Assert.AreEqual(dl, rdl);
            }
        }

        static void ThreadProcessesTimer()
        {
            Stopwatch stopWatch = new Stopwatch();
            ulong i = 0;
            ulong endTimerPoint = INTERVAL;
            uint increment = FIRST_INCREMENT;
            ulong endPoint = UINT_END;
            while(i <= ULONG_END_MARGIN)
            {
                Console.WriteLine("starting point of " + i + " with increment of " + increment + " Endpoint is " + endPoint);
                while (i <= endPoint)
                {
                    stopWatch.Start();
                    while (i <= endTimerPoint)
                    {
                        TestValue(i);
                        i += increment;
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
                endPoint = ULONG_END_MARGIN;
                increment = SECOND_INCREMENT;
            }
           
           
        }

        static void ThreadProcesses(ulong start)
        {
            ulong i = 0;
            uint increment = FIRST_INCREMENT;
            ulong endPoint = UINT_END;
            while (i <= ULONG_END_MARGIN)
            {
                while (i <= endPoint)
                {
                    TestValue(i);
                    i += increment;
                }
                endPoint = ULONG_END_MARGIN;
                increment = SECOND_INCREMENT;
            }
        }

        static void Main(string[] args)
        {
            uint cores = THREAD_SPAWN_DEFAULT;
            if (args.Length > 0)
            {
                if(uint.TryParse(args[0],out uint c))
                {
                    cores = c;
                    FIRST_INCREMENT = c;
                    SECOND_INCREMENT = FIRST_INCREMENT * 5;
                    ULONG_END_MARGIN = ulong.MaxValue - c;
                }
            }
            Console.WriteLine("Threads Creating: " + cores);
            Thread[] threads = new Thread[cores];
            for (int i = 0; i < threads.Length; i++)
            {
                if(i == 0)
                {
                    threads[i] = new Thread(new ThreadStart(ThreadProcessesTimer));
                }
                else
                {
                    ulong index = (ulong)i;
                    threads[i] = new Thread(new ThreadStart(()=> ThreadProcesses(index)));
                }

            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0;i < threads.Length;i++)
            {
                threads[i].Start();
            }

            for(int i = 0;i < threads.Length;i++)
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
    }
}
