using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LLBitManTester.ContextTest;

namespace LLBitManTester
{
    public class BaseTest
    {
        [OneTimeSetUp]
        public void StartTest()
        {

        }

        [OneTimeTearDown]
        public void EndTest()
        {
            
        }

        public static void Out(string s)
        {
            Console.WriteLine(s);
        }

        private static void ContextAllPossibleScenerios()
        {

            TestSession[] session = new TestSession[AllPossibleSceneriosTester.THREAD_SPAWN_DEFAULT + 1];
            for (int i = 0; i < session.Length; i++)
                session[i] = new TestSession();


            AllPossibleSceneriosTester t = new AllPossibleSceneriosTester((ulong u, ulong index) =>
            {
                TestPrimativeValue(session[index], u);
            });
            t.AllPossibleScenerios();
        }

        public static void Main(string[] args)
        {
            AllPossibleSceneriosTester t = new AllPossibleSceneriosTester((ulong i, ulong index) => LLBitManTester.TestValue(i));
            t.AllPossibleScenerios();
            ContextAllPossibleScenerios();
        }

    }
}
