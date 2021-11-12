using System;
using System.Collections;
using System.Collections.Generic;

namespace LLDataMan
{
    public static class LLBitMan
    {
        public static byte[] ToByteArray(this bool b) => BitConverter.GetBytes(b);

        public static byte[] ToByteArray(this byte b) => new byte[] { b };

        public static byte[] ToByteArray(this sbyte sb) => new byte[] { (byte)sb };

        public static byte[] ToByteArray(this char c) => BitConverter.GetBytes(c);

        public static byte[] ToByteArray(this float f) => BitConverter.GetBytes(f);

        public static byte[] ToByteArray(this int n) => BitConverter.GetBytes(n);

        public static byte[] ToByteArray(this uint un) => BitConverter.GetBytes(un);

        public static byte[] ToByteArray(this long ln) => BitConverter.GetBytes(ln);

        public static byte[] ToByteArray(this ulong uln) => BitConverter.GetBytes(uln);

        public static byte[] ToByteArray(this short s) => BitConverter.GetBytes(s);

        public static byte[] ToByteArray(this ushort us) => BitConverter.GetBytes(us);

        public static byte[] ToByteArray(this double dl) => BitConverter.GetBytes(dl);

        public static unsafe byte[] ToByteArray(this decimal dec)
        {
            byte[] array = new byte[128];
            int[] decBytes = Decimal.GetBits(dec);
            //Endianness should be fine aslong as it's not flip flopped
            fixed (byte * ptr = &array[0])
            {
                byte* c = ptr;
                *(int*)c = decBytes[0];
                c += sizeof(int);
                *(int*)c = decBytes[1];
                c += sizeof(int);
                *(int*)c = decBytes[2];
                c += sizeof(int);
                *(int*)c = decBytes[3];
            }
            return array;

        }

        public static unsafe bool TryToPrimative(this byte[] data,out decimal dec)
        {
            if (data != null && data.Length == 128)
            {
                int[] bits = new int[4];
                fixed (byte* ptr = &data[0])
                {
                    byte* c = ptr;
                    bits[0] = *(int*)c;
                    c += sizeof(int);
                    bits[1] = *(int*)c;
                    c += sizeof(int);
                    bits[2] = *(int*)c;
                    c += sizeof(int);
                    bits[3] = *(int*)c;
                }
                dec = new decimal(bits);
                return true;
            }
            dec = default(decimal);
            return false;
        }

        public static bool TryToPrimative(this byte data, out bool b)
        {
            b = Convert.ToBoolean(data);
            return true;
        }

        public static bool TryToPrimative(this byte[]data, out bool b)
        {
            if(data != null && sizeof(byte) == data.Length)
            {
                return data[0].TryToPrimative(out b);
            }
            b = default(bool);
            return false;
        }

        public static bool TryToPrimative(this byte[] data, out byte b) 
        {
            if (data != null&&sizeof(byte) == data.Length)
            {
                b = data[0];
                return true;
            }
            b = default(byte);
            return false;
        }

        public static bool TryToPrimative(this byte[] data, out sbyte b)
        {
            if (data != null&&sizeof(sbyte) == data.Length)
            {
                b = (sbyte)data[0];
                return true;
            }
            b = default(sbyte);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data,out char output)
        {
            if(data != null&&sizeof(char) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(char*)ptr;
                }
                return true;
            }
            output = default(char);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out float output)
        {
            if(data != null&&sizeof(float) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(float*)ptr;
                }
                return true;
            }
            output = default(float);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out int output)
        {
            if(data != null&&sizeof(int) ==data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(int*)ptr;
                }
                return true;
            }
            output = default(int);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out uint output)
        {
            if(data != null&&sizeof(uint) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(uint*)ptr;
                }
                return true;
            }
            output = default(uint);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out long output)
        {
            if(data != null&&sizeof(long) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(long*)ptr;
                }
                return true;
            }
            output = default(long);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out ulong output)
        {
            if(data != null&&sizeof(ulong) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(ulong*)ptr;
                }
                return true;
            }
            output = default(ulong);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out short output)
        {
            if(data != null&&sizeof(short) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(short*)ptr;
                }
                return true;
            }
            output = default(short);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out ushort output)
        {
            if(data != null&&sizeof(ushort) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(ushort*)ptr;
                }
                return true;
            }
            output = default(ushort);
            return false;
        }

        public static bool TryToPrimative(this byte[] data, out Guid output)
        {
            if(data != null&&data.Length == 16)
            {
                output = new Guid(data);
                return true;
            }
            output = default(Guid);
            return false;
        }

        public static unsafe bool TryToPrimative(this byte[] data, out double output)
        {
            if(data != null&&sizeof(double) == data.Length)
            {
                fixed (byte* ptr = &data[0])
                {
                    output = *(double*)ptr;
                    return true;
                }
            }
            output = default(double);
            return false;
        }
        public enum SupportedPrimativeTypes
        {
            GUID,
            BYTE,
            SBYTE,
            CHAR,
            DEC,
            FLOAT,
            INT,
            UINT,
            LONG,
            ULONG,
            DOUBLE,
            SHORT,
            USHORT,
            BOOL,
        }

        public static readonly Dictionary<Type, SupportedPrimativeTypes> TYPE_MAP = new Dictionary<Type, SupportedPrimativeTypes>
        {
            {typeof(Guid),SupportedPrimativeTypes.GUID},
            {typeof(byte),SupportedPrimativeTypes.BYTE},
            {typeof(sbyte),SupportedPrimativeTypes.SBYTE},
            {typeof(char),SupportedPrimativeTypes.CHAR},
            {typeof(decimal),SupportedPrimativeTypes.DEC},
            {typeof(float),SupportedPrimativeTypes.FLOAT},
            {typeof(int),SupportedPrimativeTypes.INT},
            {typeof(uint),SupportedPrimativeTypes.UINT},
            {typeof(long),SupportedPrimativeTypes.LONG},
            {typeof(ulong),SupportedPrimativeTypes.ULONG},
            {typeof(double),SupportedPrimativeTypes.DOUBLE},
            {typeof(short),SupportedPrimativeTypes.SHORT},
            {typeof(ushort),SupportedPrimativeTypes.USHORT},
            {typeof(bool), SupportedPrimativeTypes.BOOL }
        };

        public static bool TryGetPrimativeValue<TResult>(byte[] data, out TResult t) where TResult : struct
        {

            if (data != null && TYPE_MAP.TryGetValue(typeof(TResult), out SupportedPrimativeTypes type))
            {
                switch (type)
                {
                    case SupportedPrimativeTypes.GUID:
                        if (data.TryToPrimative(out Guid g))
                        {
                            t = (TResult)(g as IFormattable);
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.BYTE:
                        if (data.TryToPrimative(out byte r))
                        {
                            t = (TResult)(r as IConvertible);
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.SBYTE:
                        if (data.TryToPrimative(out sbyte sb))
                        {
                            t = (TResult)(sb as IConvertible);
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.CHAR:
                        if (data.TryToPrimative(out char rc))
                        {
                            IConvertible c = rc;
                            t = (TResult)c;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.DEC:
                        if (data.TryToPrimative(out decimal dec))
                        {
                            IConvertible c = dec;
                            t = (TResult)c;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.FLOAT:
                        if (data.TryToPrimative(out float f))
                        {
                            IConvertible flt = f;
                            t = (TResult)flt;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.INT:
                        if (data.TryToPrimative(out int intt))
                        {
                            IConvertible dd = intt;
                            t = (TResult)dd;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.UINT:
                        if (data.TryToPrimative(out uint uintv))
                        {
                            IConvertible dd = uintv;
                            t = (TResult)dd;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.LONG:
                        if (data.TryToPrimative(out long l))
                        {
                            IConvertible ll = l;
                            t = (TResult)ll;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.ULONG:
                        if (data.TryToPrimative(out ulong ul))
                        {
                            IConvertible uul = ul;
                            t = (TResult)uul;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.DOUBLE:
                        if (data.TryToPrimative(out double dl))
                        {
                            IConvertible ddl = dl;
                            t = (TResult)ddl;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.SHORT:
                        if (data.TryToPrimative(out short s))
                        {
                            IConvertible ss = s;
                            t = (TResult)ss;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.USHORT:
                        if (data.TryToPrimative(out ushort us))
                        {
                            IConvertible uus = us;
                            t = (TResult)uus;
                            return true;
                        }
                        break;
                    case SupportedPrimativeTypes.BOOL:
                        if(data.TryToPrimative(out bool b))
                        {
                            IConvertible bb = b;
                            t = (TResult)bb;
                            return true;
                        }
                        break;
                }
            }

            t = default(TResult);
            return false;
        }
    }
}
