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
        private const uint THREAD_SPAWN = 8;
        private const ulong END_TIMER_POINT = 100000000;
        private const ulong END = ulong.MaxValue;



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
            ulong endTimerPoint = END_TIMER_POINT;
            while(i <= END)
            {
                stopWatch.Start();
                while (i <= endTimerPoint)
                {
                    TestValue(i);
                    i += THREAD_SPAWN;
                }
                endTimerPoint += END_TIMER_POINT;
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine("RunTime Till End_Point " + ts.TotalMilliseconds + "ms");
                Console.WriteLine("Continue with normal run");
                stopWatch.Reset();
            }
           
        }

        static void ThreadProcesses(ulong start)
        {
            for (ulong i = start; i <= END; i += THREAD_SPAWN)
            {
                TestValue(i);
            }
        }

        static void Main(string[] args)
        {
            Thread[] threads = new Thread[THREAD_SPAWN];
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
