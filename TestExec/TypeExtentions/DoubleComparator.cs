using System;
using System.Collections.Generic;
 
using System.Text;

namespace OpticalBuilderLib.TypeExtentions
{
    public static class DoubleComparator
    {
        static double eps = 0.000001;
        /// <summary>
        /// погрешность сравнения
        /// </summary>
        public static double EPS
        {
            get { return eps; }
            set 
            {
                if (value > 0)
                    if ((value <= 0.001) && (value >= 0.000001))
                        eps = value;
            }
        }
        /// <summary>
        /// Возвращает, равны ли две переменные
        /// </summary>
        /// <param name="a">Переменная a</param>
        /// <param name="b">Переменная b</param>
        /// <returns>Если равны - true, если нет - false</returns>
        public static bool IsEqual(double a, double b)
        {
            if (Math.Abs(a - b) < eps) return true;
            else return false;
        }
        /// <summary>
        /// Возвращает, меньше ли переменная a переменной b
        /// </summary>
        /// <param name="a">Переменная a</param>
        /// <param name="b">Переменная b</param>
        /// <returns>Если меньше - true, если нет - false</returns>
        public static bool IsLower(double a, double b)
        {
            if ((a - b) <= -eps) return true;
            else return false;
        }
        /// <summary>
        /// Возвращает, больше ли переменная a переменной b
        /// </summary>
        /// <param name="a">Переменная a</param>
        /// <param name="b">Переменная b</param>
        /// <returns>Если больше - true, если нет - false</returns>
        public static bool IsGreater(double a, double b)
        {
            if ((a - b) >= eps) return true;
            else return false;
        }
        /// <summary>
        /// Возвращает математический знак - результат сравнения
        /// </summary>
        /// <param name="a">Переменная a</param>
        /// <param name="b">Переменная b</param>
        /// <returns></returns>
        public static char MathSignReturn(double a, double b)
        {
            if (IsEqual(a, b)) return '=';
            if (IsGreater(a, b)) return '>';
            if (IsLower(a, b)) return '<';
            //Exception e = new Exception("Внутренняя программная ошибка. Попробуйте перезапуск.");
            throw new Exceptions.InnerException();
        }
        /// <summary>
        /// Возвращает, равны ли две переменные
        /// </summary>
        /// <param name="a">Переменная a</param>
        /// <param name="b">Переменная b</param>
        /// <returns>Если равны - false, если нет - true</returns>
        public static bool IsNotEqual(double a, double b)
        {
            if (Math.Abs(a - b) < eps) return false;
            else return true;
        }
        /// <summary>
        /// Возвращает, меньше ли переменная a переменной b
        /// </summary>
        /// <param name="a">Переменная a</param>
        /// <param name="b">Переменная b</param>
        /// <returns>Если меньше - false, если нет - true</returns>
        public static bool IsNotLower(double a, double b)
        {
            if ((a - b) <= -eps) return false;
            else return true;
        }
        /// <summary>
        /// Возвращает, больше ли переменная a переменной b
        /// </summary>
        /// <param name="a">Переменная a</param>
        /// <param name="b">Переменная b</param>
        /// <returns>Если больше - false, если нет - true</returns>
        public static bool IsNotGreater(double a, double b)
        {
            if ((a - b) >= eps) return false;
            else return true;
        }
    }
}
