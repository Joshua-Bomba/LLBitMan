using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            System.Diagnostics.Debug.WriteLine(s);
        }

    }
}
