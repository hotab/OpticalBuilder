using System;
using System.Drawing;
using OpticalBuilderLib.MathLib;
namespace OpticalBuilderLib.OpticalObjects
{
    public class BrightPoint:ObjectProto
    {
        public event EventHandler<EventArgs> Removal; //Оповіщення променів про видаленя точки
        public event EventHandler<EventArgs> NameChange; //Оповіщення променів про зміну імені точки
        public BrightPoint(string Name, SystemCoordinates Coordinates) //Конструктор для джерела
        {
            name = Name;
            coordinates = Coordinates;
        }
        public BrightPoint(string SaveString) //Конструктор для джерела - читання з файлу
            : base()
        {
            string[] splitted;
            splitted = SaveString.Split('^');
            if (splitted.Length != 5) //Пошкоджений файл - критична помилка, програма закривається
                throw new OpticalBuilderLib.Exceptions.ErrorWhileReading();
            try
            {
                name = splitted[1];
                coordinates = new SystemCoordinates(Convert.ToDouble(splitted[2]), Convert.ToDouble(splitted[3]));
                id = Convert.ToInt32(splitted[4]);
            }
            catch
            {
                throw new OpticalBuilderLib.Exceptions.ErrorWhileReading();//Пошкоджений файл - критична помилка, програма закривається
            }
        }
        public override void Drawer()
        {
            Drawing.OGL.DrawFilledCircle(new Pen(Selected ? Color.Blue : Color.Black, 6), coordinates, 6); //Малювання точки за допомогою OpenGL
        }
        public override string GenerateSaveString()
        {
            string SaveStr = GetTypeSpecifier() + "^" + Name + "^" + Coordinates.X.ToString() + "^" + Coordinates.Y.ToString() + "^" + id.ToString();
            //Строка зберігання - для джерела = специфікатор^ім'я^координата Х^координата Y^ідентифікаційний номер
            return SaveStr;
        }
        public override SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray)
        {
            //Ніколи не перетнеться з прямою за очевидних причин
            PointFound = false;
            angle_got = false;
            anglee = Angle.Zero;
            return SystemCoordinates.Zero;
        }
        public override string GetTypeSpecifier()
        {
            return GetSpec(4); //Отримання специфікатору
        }
        public override int DistanceToPointS(Point X)
        {
            return (int)Math.Ceiling(CoordinateSystem.Instance.Converter(X).Distance(coordinates) * CoordinateSystem.Instance.Scale); //Відстань до точки X на simpleOpenGLControl у 
        }
        public override void BeingRemoved()
        {
            if(Removal!=null) Removal.Invoke(this, new EventArgs());
            base.BeingRemoved();
        }
        public void InvokeNameChange()
        {
            if(NameChange!=null) NameChange.Invoke(this, new EventArgs());
        }
    }
}
