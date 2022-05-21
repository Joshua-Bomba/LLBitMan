﻿using LLBitMan;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLBitManTester
{
    [TestFixture]
    public class WrappedTest : BaseTest
    {
        [Test]
        public void WrappedContentScenerio()
        {

        }

        static void TestWrappedScenerio(ulong index)
        {
            if (index <= 1)
            {
                bool bl = index != 0;
                byte[] bla = bl.ToByteArray();
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(bla, out bool rbl));
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

                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(ga, out Guid rg));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(ba, out byte rb));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(sba, out sbyte rsb));
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

                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(sa, out short ra));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(usa, out ushort rus));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(ca, out char rc));
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

                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(fa, out float rf));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(ia, out int ri));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(uia, out uint rui));
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
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(da, out decimal rd));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(la, out long rl));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(ula, out ulong rul));
                Assert.IsTrue(LLByteArrayManager.TryGetPrimitiveValue(dla, out double rdl));
                Assert.AreEqual(d, rd);
                Assert.AreEqual(l, rl);
                Assert.AreEqual(ul, rul);
                Assert.AreEqual(dl, rdl);
            }
        }


    }
}
