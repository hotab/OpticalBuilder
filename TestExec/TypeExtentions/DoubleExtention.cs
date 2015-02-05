using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.TypeExtentions
{
    public struct DoubleExtention:IComparable,IConvertible
    {
        private double x;
        public double X { get { return x; } set {x = value;}}

        #region Functional

        public DoubleExtention sqrt()
        {
            return Math.Sqrt(this);
        }
        public void sqrt_set()
        {
            x = Math.Sqrt(x);
        }
        public int GetIntegerPart()
        {
            int y = (int)(Math.Floor(x));
            if (this == (DoubleExtention)(y + 1))
                return y + 1;
            else return y;
        }
        public DoubleExtention GetFractionalPart()
        {
            return this - (DoubleExtention)GetIntegerPart();
        }
        public int GetFractionalPart(int precision)
        {
            int z = 0;
            double y = x;
            if (precision < 0)
            {
                System.Windows.Forms.MessageBox.Show((new OpticalBuilderLib.Exceptions.PrecisionError(1)).Message);
                return 0;
            }
            if (precision > 8)
            {
            }
            if (precision == 0) return 0;
            while (precision > 0)
            {
                y *= 10;
                precision--;
            }
            z = (int)(Math.Floor(y));
            if (y == (DoubleExtention)(z + 1))
                return z + 1;
            else return z;

        }
        #endregion
        #region Constructors

        public DoubleExtention(double y)
        {
            //x = y;
            if(y.ToString() == "NaN")
                y = 0;
            x = y;
        }
        public DoubleExtention(DoubleExtention y)
        {
            x = y.X;
        }

        #endregion
        #region Covertions

        public System.TypeCode GetTypeCode()
        {
            return System.TypeCode.Double;
        }
        public bool ToBoolean(System.IFormatProvider X)
        {
            return Convert.ToBoolean(x, X);
        }
        public char ToChar(System.IFormatProvider X)
        {
            return Convert.ToChar(x, X);
        }
        public sbyte ToSByte(System.IFormatProvider X)
        {
            return Convert.ToSByte(x, X);
        }
        public byte ToByte(System.IFormatProvider X)
        {
            return Convert.ToByte(this, X);
        }
        public Int16 ToInt16(System.IFormatProvider X)
        {
            return Convert.ToInt16(this, X);
        }
        public Int32 ToInt32(System.IFormatProvider X)
        {
            return Convert.ToInt32(this, X);
        }
        public Int64 ToInt64(System.IFormatProvider X)
        {
            return Convert.ToInt64(this, X);
        }
        public UInt16 ToUInt16(System.IFormatProvider X)
        {
            return Convert.ToUInt16(this, X);
        }
        public UInt32 ToUInt32(System.IFormatProvider X)
        {
            return Convert.ToUInt32(this, X);
        }
        public UInt64 ToUInt64(System.IFormatProvider X)
        {
            return Convert.ToUInt64(this, X);
        }
        public Single ToSingle(System.IFormatProvider X)
        {
            return Convert.ToSingle(this, X);
        }
        public double ToDouble(System.IFormatProvider X)
        {
            return Convert.ToDouble(this, X);
        }
        public decimal ToDecimal(System.IFormatProvider X)
        {
            return Convert.ToDecimal(this, X);
        }
        public DateTime ToDateTime(System.IFormatProvider X)
        {
            return Convert.ToDateTime(this, X);
        }
        public string ToString(System.IFormatProvider X)
        {
            return Convert.ToString(this.x, X);
        }
        public object ToType(System.Type t, System.IFormatProvider X)
        {
            throw new InvalidCastException();
        }
        public bool ToBoolean()
        {
            return Convert.ToBoolean(x);
        }
        public char ToChar()
        {
            return Convert.ToChar(x);
        }
        public sbyte ToSByte()
        {
            return Convert.ToSByte(x);
        }
        public byte ToByte()
        {
            return Convert.ToByte(this.x);
        }
        public Int16 ToInt16()
        {
            return Convert.ToInt16(this.x);
        }
        public Int32 ToInt32()
        {
            return Convert.ToInt32(this.x);
        }
        public Int64 ToInt64()
        {
            return Convert.ToInt64(this.x);
        }
        public UInt16 ToUInt16()
        {
            return Convert.ToUInt16(this.x);
        }
        public UInt32 ToUInt32()
        {
            return Convert.ToUInt32(this.x);
        }
        public UInt64 ToUInt64()
        {
            return Convert.ToUInt64(this.x);
        }
        public Single ToSingle()
        {
            return Convert.ToSingle(this.x);
        }
        public double ToDouble()
        {
            return Convert.ToDouble(this.x);
        }
        public decimal ToDecimal()
        {
            return Convert.ToDecimal(this.x);
        }
        public DateTime ToDateTime()
        {
            return Convert.ToDateTime(this.x);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return x.ToString("0.000000");
        }
        #endregion
        #region Operators
        public override bool Equals(object obj)
        {
            if (CompareTo(obj) == 0) return true;
            else return false;
            
        }
        public static DoubleExtention operator +(DoubleExtention a, DoubleExtention b)
        {
            return new DoubleExtention(a.X + b.X);
        }
        public static DoubleExtention operator -(DoubleExtention a, DoubleExtention b)
        {
            return new DoubleExtention(a.X - b.X);
        }
        public static DoubleExtention operator -(DoubleExtention a)
        {
            return new DoubleExtention(-a.X);
        }
        public static DoubleExtention operator *(DoubleExtention a, DoubleExtention b)
        {
            return new DoubleExtention(a.X * b.X);
        }
        public static DoubleExtention operator /(DoubleExtention a, DoubleExtention b)
        {
            return new DoubleExtention(a.X / b.X);
        }
        public static bool operator ==(DoubleExtention a, DoubleExtention b)
        {
            return DoubleComparator.IsEqual(a, b);
        }
        public static bool operator !=(DoubleExtention a, DoubleExtention b)
        {
            return DoubleComparator.IsNotEqual(a, b);
        }
        public static bool operator >(DoubleExtention a, DoubleExtention b)
        {
            if (DoubleComparator.IsGreater(a, b)) return true; else return false;
        }
        public static bool operator >=(DoubleExtention a, DoubleExtention b)
        {
            if (DoubleComparator.IsNotLower(a, b)) return true; else return false;
        }
        public static bool operator <(DoubleExtention a, DoubleExtention b)
        {
            if (DoubleComparator.IsLower(a, b)) return true; else return false;
        }
        public static bool operator <=(DoubleExtention a, DoubleExtention b)
        {
            if (DoubleComparator.IsNotGreater(a, b)) return true; else return false;
        }
        public static DoubleExtention MaximumOf(params DoubleExtention[] a)
        {
            Double answer = a[0];
            for (int i = 1; i < a.Length; i++)
                if (a[i] > answer) answer = a[i];
            return answer;
        }
        public static DoubleExtention MinimumOf(params DoubleExtention[] a)
        {
            Double answer = a[0];
            for (int i = 1; i < a.Length; i++)
                if (a[i] < answer) answer = a[i];
            return answer;
        }
        public static DoubleExtention operator ++(DoubleExtention a)
        {
            return new DoubleExtention(a + 1);
        }
        public static DoubleExtention operator --(DoubleExtention a)
        {
            return new DoubleExtention(a - 1);
        }
        public int CompareTo(object obj)
        {
            if (obj is DoubleExtention)
            {
                char sign = DoubleComparator.MathSignReturn((DoubleExtention)obj, this);
                if (sign == '=') return 0;
                if (sign == '>') return 1;
                if (sign == '<') return -1;
            }
            else
                throw new ArgumentException();
            return 0;
        }

        #endregion
        #region Type Operators

        public static implicit operator double(DoubleExtention e)
        {
            return e.X;
        }
        public static implicit operator DoubleExtention(double e)
        {
            return new DoubleExtention(e);
        }
        public static implicit operator DoubleExtention(int e)
        {
            return new DoubleExtention(Convert.ToDouble(e));
        }
        public static explicit operator int(DoubleExtention e)
        {
            return (int)(e.X);
        }
        public static explicit operator Int64(DoubleExtention e)
        {
            return Convert.ToInt64(e.x);
        }
        public static implicit operator DoubleExtention(Int64 e)
        {
            return new DoubleExtention(Convert.ToDouble(e));
        }
        public static implicit operator bool(DoubleExtention a)
        {
            if (a == 0) return false;
            return true;
        }
        public static implicit operator DoubleExtention(bool a)
        {
            if (a) return 1;
            else return 0;
        }
        #endregion
    }
}
