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
        public DefaultHttpContext context = new DefaultHttpContext();
        [Test]
        public void EnqueDequeTest()
        {
            
        }

    }
}
