using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LLBitMan
{
    public static class LLBitManSessionExtensions
    {
        public static void SetPrimative<TProp>(this ISession session, string key, TProp value)
        {
            if (value == null)
            {
                session.Remove(key);
                return;
            }
            else if (LLByteArrayManager.TrySetPrimativeValue(value, out byte[] d))
            {
                session.Set(key, d);
                return;
            }

            throw new InvalidDataException();
        }
        public static bool TrySetPrimative<TProp>(this ISession session, string key, TProp value)
        {
            if (value == null)
            {
                session.Remove(key);
                return true;
            }

            if (value != null && LLByteArrayManager.TrySetPrimativeValue(value, out byte[] d))
            {
                session.Set(key, d);
                return true;
            }

            return false;
        }
        public static TResult GetPrimative<TResult>(this ISession session, string key)
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

        public static bool TryGetPrimative<TResult>(this ISession session, string key, out TResult value)
        {
            if (session.TryGetValue(key, out byte[] t))
            {
                if (t != null)
                {
                    if (LLByteArrayManager.TryGetPrimitiveValue<TResult>(t, out TResult r))
                    {
                        value = r;
                        return true;
                    }

                }
            }
            value = default(TResult);
            return false;
        }

        public static void SetObject<TProp>(this ISession session, string key, TProp value)
        {
            if (!session.TrySetPrimative<TProp>(key, value)) {
                string cereal = JsonConvert.SerializeObject(value);
                byte[] byteData = Encoding.UTF8.GetBytes(cereal);
                session.Set(key, byteData);
            }
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
                    try
                    {
                        string cereal = Encoding.UTF8.GetString(t);
                       TResult res =  JsonConvert.DeserializeObject<TResult>(cereal);
                        return res;
                    }
                    catch 
                    {

                    }

                }
            }
            return default(TResult);
        }

        public static bool TryGetObject<TResult>(this ISession session, string key, out TResult result)
        {
            if (session.TryGetValue(key, out byte[] t))
            {
                if (t != null)
                {
                    if (LLByteArrayManager.TryGetPrimitiveValue<TResult>(t, out TResult r))
                    {
                        result = r;
                        return true;
                    }
                    try
                    {
                        string cereal = Encoding.UTF8.GetString(t);
                        TResult res = JsonConvert.DeserializeObject<TResult>(cereal);
                        result = res;
                        return true;
                    }
                    catch
                    {

                    }

                }
            }
            result = default(TResult);
            return false;
        }
    }
}
