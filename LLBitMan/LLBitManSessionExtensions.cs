using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LLBitMan
{
    public static class LLBitManSessionExtensions
    {
        public static void SetObject<TProp>(this ISession session, string key, TProp value)
        {
            if(value == null)
            {
                session.Remove(key);
                return;
            }
            
            if (LLByteArrayManager.TrySetPrimativeValue(value, out byte[] d))
            {
                session.Set(key, d);
                return;
            }
            string cereal = JsonConvert.SerializeObject(value);
            byte[] byteData = Encoding.UTF8.GetBytes(cereal);
            session.Set(key, byteData);
        }

        public static TResult GetObject<TResult>(this ISession session, string key)
        {
            if (session.TryGetValue(key, out byte[] t))
            {
                if (t != null)
                {
                    if (LLByteArrayManager.TryGetPrimitiveValue<TResult>(t, out TResult r))
                    {
                        return r;
                    }
                }
            }
            return default(TResult);
        }

        public static bool TryGetObject<TResult>(this ISession session, string key, out TResult result)
        {
            if (session.TryGetValue(key, out byte[] t))
            {
                if(t != null)
                {
                    if (LLByteArrayManager.TryGetPrimitiveValue<TResult>(t, out TResult r))
                    {
                        result = r;
                        return true;
                    }
                }               
            }
            result = default(TResult);
            return false;
        }
    }
}
