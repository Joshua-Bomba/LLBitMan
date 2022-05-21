using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LLBitManTester
{
    public class AllPossibleSceneriosTester
    {
        public static readonly uint THREAD_SPAWN_DEFAULT = (uint)Environment.ProcessorCount;
        private static uint INCREMENT = THREAD_SPAWN_DEFAULT;
        private const ulong INTERVAL = 100000000;
        private const ulong UINT_END = uint.MaxValue;
        private Action<ulong,ulong> _f;
        public AllPossibleSceneriosTester(Action<ulong,ulong> f)
        {
            _f = f;
        }

        public void AllPossibleScenerios()
        {
            uint cores = THREAD_SPAWN_DEFAULT;
            BaseTest.Out("Threads Creating: " + cores);
            Thread[] threads = new Thread[cores];
            for (int i = 0; i < threads.Length; i++)
            {
                ulong index = (ulong)i;
                if (i == 0)
                {
                    threads[i] = new Thread(new ThreadStart(() => ThreadProcessesTimer(index)));
                }
                else
                {
                    
                    threads[i] = new Thread(new ThreadStart(() => ThreadProcesses(index)));
                }

            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            PastUIntMax();

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            BaseTest.Out("Full Run Time " + elapsedTime);
            stopWatch.Stop();
        }

        protected void PastUIntMax()
        {
            ulong endPoint = ulong.MaxValue;
            ulong i = uint.MaxValue;
            while (i < endPoint)
            {
                i = (i << 4);
                for (byte k = 0; k < 15; k++)
                {
                    _f(i, THREAD_SPAWN_DEFAULT);
                    i += 1;
                }
                _f(i, THREAD_SPAWN_DEFAULT);
            }
        }

        protected void ThreadProcessesTimer(ulong index)
        {
            Stopwatch stopWatch = new Stopwatch();
            ulong i = 0;
            ulong endTimerPoint = INTERVAL;
            ulong endPoint = UINT_END;
            BaseTest.Out("starting point of " + i + " with increment of " + INCREMENT + " Endpoint is " + endPoint);
            while (i <= endPoint)
            {
                stopWatch.Start();
                while (i <= endTimerPoint)
                {
                    _f(i,index);
                    i += INCREMENT;
                }

                endTimerPoint += INTERVAL;
                if (endTimerPoint > endPoint)
                {
                    endTimerPoint = endPoint;
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                BaseTest.Out("RunTime Till INTERVAL of " + i + " took " + ts.TotalMilliseconds + "ms");
                stopWatch.Reset();
            }

        }

        protected void ThreadProcesses(ulong start)
        {
            ulong i = 0;
            uint increment = INCREMENT;
            ulong endPoint = UINT_END;
            while (i <= endPoint)
            {
                _f(i,start);
                i += increment;
            }
        }
    }
}
