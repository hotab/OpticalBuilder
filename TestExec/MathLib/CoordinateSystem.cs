using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.TypeExtentions;
namespace OpticalBuilderLib.MathLib
{
    public class CoordinateSystem
    {
        #region Variables
        private static CoordinateSystem instance;
        private static bool created = false;
        private bool sizesSet;
        private int centerX;
        private int centerY;
        private int scale;
        private int screenWidth;
        private int screenHeight;
        private DoubleExtention differenceX;
        private DoubleExtention differenceY;
        public int CenterX { get { return centerX; } }
        public int CenterY { get { return centerY; } }
        public int Scale
        {
            get { return scale; } 
            set { scale = Math.Max(1,Math.Min(value, 250)); }
        }
        public int ScreenWidth { get { return screenWidth; } }
        public int ScreenHeight { get { return screenHeight; } }
        public bool SizesSet { get { return sizesSet; } }
        public DoubleExtention MaxY
        {
            get
            {
                return Converter(screenWidth, 0).Y;
            }
        }
        public DoubleExtention MinY
        {
            get
            {
                return Converter(screenWidth, screenHeight).Y;
            }
        }
        public DoubleExtention MaxX
        {
            get
            {
                return Converter(screenWidth, screenHeight).X;
            }
        }
        public DoubleExtention MinX
        {
            get
            {
                return Converter(0, screenHeight).X;
            }
        }
        public DoubleExtention DifferenceX
        {
            get { return differenceX; }
            set { differenceX = value; }
        }
        public DoubleExtention DifferenceY
        {
            get { return differenceY; }
            set { differenceY = value; }
        }
        public SystemCoordinates DifferenceSize
        {
            get { return new SystemCoordinates(differenceX, differenceY);}
            set 
            { 
                differenceX = value.X;
                differenceY = value.Y;
            }
        }
        public static CoordinateSystem Instance
        {
            get
            {
                return GetInstance();
            }
        }
        #endregion
        private CoordinateSystem()
        {
            differenceX = 0;
            differenceY = 0;
            sizesSet = false;
            Configuration.Config.NewConfig += new EventHandler<EventArguments.ConfigurationChangeArgs>(Config_NewConfig);
        }

        void Config_NewConfig(object sender, EventArguments.ConfigurationChangeArgs e)
        {
            if(e.FullReload)
            {
            }
        }
        /// <summary>
        /// Получить коориднатную систему (синглтон)
        /// </summary>
        /// <returns>Инстанс координатной системы</returns>
        static CoordinateSystem GetInstance()
        {
            if (created == false) { instance = new CoordinateSystem(); created = true; }
            return instance;
        }
        #region Coordinate System Functional
        /// <summary>
        /// Метод для установки параметров коориднатной системы
        /// </summary>
        /// <param name="Scale">Масштаб системы (единица размера системы в пикселях экрана)</param>
        /// <param name="CenterX">Центр системы относительно элемента, на котором будут рисовать (верхний левый угол - 0,0) в пикселях (координата Х)</param>
        /// <param name="CenterY">Центр системы относительно элемента, на котором будут рисовать (верхний левый угол - 0,0) в пикселях (координата Y)</param>
        /// <param name="Width">Ширина элемента рисования</param>
        /// <param name="Height">Высота элемента рисования</param>
        public void ResetSizes(int Scale, int CenterX, int CenterY, int Width, int Height)
        {
            centerX = CenterX;
            centerY = CenterY;
            scale = Scale;
            screenWidth = Width;
            screenHeight = Height;
            sizesSet = true;
        }
        /// <summary>
        /// Метод для установки параметров коориднатной системы
        /// </summary>
        /// <param name="Scale">Масштаб системы (единица размера системы в пикселях экрана)</param>
        /// <param name="Center">Центр системы относительно элемента, на котором будут рисовать (верхний левый угол - 0,0) в пикселях</param>
        /// <param name="Sizes">Размер эелемента рисования</param>
        public void ResetSizes(int Scale, Point Center, Point Sizes)
        {
            ResetSizes(Scale, Center.X, Center.Y, Sizes.X, Sizes.Y);
        }
        /// <summary>
        /// Метод для установки параметров коориднатной системы
        /// </summary>
        /// <param name="Scale">Масштаб системы (единица размера системы в пикселях экрана)</param>
        /// <param name="Center">Центр системы относительно элемента, на котором будут рисовать (верхний левый угол - 0,0) в пикселях</param>
        /// <param name="Width">Ширина элемента рисования</param>
        /// <param name="Height">Высота элемента рисования</param>
        public void ResetSizes(int Scale, Point Center, int Width, int Height)
        {
            ResetSizes(Scale, Center.X, Center.Y, Width, Height);
        }
        /// <summary>
        /// Конвертация координат из системных в экранные и наоборот
        /// </summary>
        /// <param name="SystemX">Системный Х</param>
        /// <param name="SystemY">Системный Y</param>
        /// <returns>Точка на элементе рисования</returns>
        public Point Converter(DoubleExtention SystemX, DoubleExtention SystemY)
        {
            if (sizesSet == false)
            {
                Exception e = new Exception("Система не инициализирована. Выполните команду ObjectCollection.GetSystem().ResetSizes()");
                throw e;
            }
            SystemX -= differenceX;
            SystemY -= differenceY; 
            int ScreenX = (int)(SystemX*scale) + centerX; 
            int ScreenY = (screenHeight-(int)(SystemY*scale)) - centerY;
            return new Point(ScreenX, ScreenY);
        }
        /// <summary>
        /// Конвертация координат из системных в экранные и наоборот
        /// </summary>
        /// <param name="ScreenPoint">Точка на экране</param>
        /// <returns></returns>
        public SystemCoordinates Converter(Point ScreenPoint)
        {
            return Converter(ScreenPoint.X, ScreenPoint.Y);
        }
        /// <summary>
        /// Конвертация координат из системных в экранные и наоборот
        /// </summary>
        /// <param name="X">Точка на экране (координата X)</param>
        /// <param name="Y">Точка на экране (координата Y)</param>
        /// <returns></returns>
        public SystemCoordinates Converter(int X, int Y)
        {
            DoubleExtention x = X, y = Y;
            x -= centerX;
            x /= scale;
            y = screenHeight - y;
            y -= centerY;
            y /= scale;
            x += differenceX;
            y += differenceY;
            return new SystemCoordinates(x, y);
        }
        /// <summary>
        /// Конвертация координат из системных в экранные и наоборот
        /// </summary>
        /// <param name="Point">Точка в системе</param>
        /// <returns></returns>
        public Point Converter(SystemCoordinates Point)
        {
            return Converter(Point.X, Point.Y);
        }

        #endregion
        #region Event Listeners
        public void ImageReset(object sender, EventArguments.ImageChangeArgs e)
        {
            ResetSizes(scale, new Point(e.NewSize.Width / 2, e.NewSize.Height / 2), e.NewSize.Width, e.NewSize.Height);
        }
        #endregion
    }
}
