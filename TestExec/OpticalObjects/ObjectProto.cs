using System;
using System.Drawing;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.MathLib;

namespace OpticalBuilderLib.OpticalObjects
{
    public abstract class ObjectProto //Прототип об'єкту
    {
        public event EventHandler<EventArgs> ObjectChanged; //Об'єкт змінився - подія
        public void RaiseChanged() //Об'єкт змінився - пробудити подію
        {
            if(ObjectChanged!=null) ObjectChanged.Invoke(this, new EventArgs());
        }
        protected string name;
        public string Name //Зміна та отримання імени об'єкту
        {
            get { return name; } 
            set
            {
                if (this is Ray) //Якщо об'єкт - промінь
                {
                    if (!(ObjectCollection.Instance.RayExists(value))) //І такого імени не існує
                    {
                        name = value; //Задаємо ім'я
                        ObjectCollection.Instance.RaiseObjectsChange(); //Вказуємо на зміну об'єктів
                    }
                }
                else //Якщо об'єкт - не промінь
                    if (!(ObjectCollection.Instance.ObjectExists(value))) //І такого імени не існує
                {
                    name = value; //Задаємо ім'я

                    if (this is BrightPoint) //Якщо об'єкт - джерело світла
                    {
                        ((BrightPoint)this).InvokeNameChange(); //Вказуємо проміням на зміну імени джерела світла
                    }
                    ObjectCollection.Instance.RaiseObjectsChange(); //Вказуємо на зміну об'єктів
                }
                RaiseChanged(); //Вказуємо на даного об'єкту
                
                
            }
        }
        protected int id;
        public bool Selected = false;
        public virtual int ID //Внутрішьнопрограмний ідентифікаційний номер
        {
            get
            {
                return id;
            }
            set
            {
                if ((ObjectCollection.Instance.ObjMaxID == value) || (ObjectCollection.Instance.Allow_List_Access == true))
                    id = value;
                RaiseChanged();
                ObjectCollection.Instance.RaiseObjectsChange();
            }
        }
        protected SystemCoordinates coordinates;
        public SystemCoordinates Coordinates //Координати центру об'єкту
        {
            get { return coordinates; } //отримання
            set
            {
                if(!(this is Ray)) MoveTo(value, coordinates); //якщо об'єкт не промінь - переміщуємо зі старих до нових координат
                RaiseChanged(); //Оповіщуємо про зміну об'єктів
                ObjectCollection.Instance.RaiseObjectsChange();
            }
        }
        public abstract string GetTypeSpecifier(); //Отримання специфікатора типу - реалізується у самих об'єктах
        public abstract void Drawer(); //Малючання об'єкту на моніторі - реалізується у самих об'єктах
        public abstract string GenerateSaveString(); //Генерування строки зберігання - реалізується у самих об'єктах
        public abstract SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray); //Перетин з променем
        public override string ToString()//Метод конвертації об'єкта в строку - внтурішньопрограмний
        {
            return name;
        }
        public static string GenName(string typespec, bool ray = false) //Метод генерування імен об'єктів автоматично
        {
            string SubName = typespec.Substring(1);
            SubName = SubName.Substring(0, SubName.Length - 1); //Отримуємо підстроку специфікатора
            SubName = STranslation.T[SubName]; //Переводимо на обрану користувачем мову
            bool gened = false;
            for(int i = 1; !gened; i++) //доки не знайдемо вільного імени - шукаємо його 
            {
                if (!ray) //якщо не промінь
                {
                    if (!(ObjectCollection.Instance.ObjectExists(SubName + i))) //ім'я знайдено - виходимо з метода
                    {
                        gened = true;
                        return SubName + i.ToString();
                    }
                }
                else //інакше
                {
                    if (!(ObjectCollection.Instance.RayExists(SubName + i))) //ім'я знайдено - виходимо з метода
                    {
                        gened = true;
                        return SubName + i.ToString();
                    }
                }
            }
            return SubName; // ніколи не визоветься, але потрібно компілятору
        }
        public abstract int DistanceToPointS(Point X);
        public virtual bool IsPreloml() //Чи є даний об'єкт линзою чи кулею? - переписується у самому об'єкті
        {
            return false;
        }
        public virtual void MoveTo(SystemCoordinates to, SystemCoordinates from) //переміщення об'єкту з одних координат в інші - дописується у об'єктах
        {
            coordinates = new SystemCoordinates(coordinates.X + to.X - from.X, coordinates.Y + to.Y - from.Y);
        }
        public virtual void BeingRemoved() //Дії при видаленні об'єкту
        {
            
        }
        public virtual void Rotate(SystemCoordinates to, SystemCoordinates from) //Поворот (по куту між вектором A(coordinates, from) и вектором B(coordinates, to)
        {
            
        }
        public static string GetSpec(int id) //Генерування специфікатора за номером (не плутать з ідентифікатором об'єкту)
        {
            if (id == 1) return "{Ray}";
            if (id == 2) return "{Mirror}";
            if (id == 3) return "{Sphere}";
            if (id == 4) return "{BrightPoint}";
            if (id == 5) return "{Lense}";
            if (id == 6) return "{SphericalMirror}";
            if (id == 7) return "{Polygon}";
            return "{null}";
        }
        public static string GetSpec(ObjectTypes type) //Генерування специфікатора за типом 
        {
            if (type == ObjectTypes.Ray) //Промінь
                return GetSpec(1);
            if (type == ObjectTypes.Mirror) //Дзеркало
                return GetSpec(2);
            if (type == ObjectTypes.Sphere) //Куля
                return GetSpec(3);
            if (type == ObjectTypes.BrightPoint) //Джерело світла
                return GetSpec(4);
            if (type == ObjectTypes.Lense) //Линза
                return GetSpec(5);
            if (type == ObjectTypes.SphereMirror) //Линза
                return GetSpec(6);
            if (type == ObjectTypes.Polygon) //Багатокутник
                return GetSpec(7);
            return GetSpec(-1); //жоден з типів
        }
    }
    public enum ObjectTypes //Типи об'єктів
    {
        Ray = 1,
        Mirror = 2,
        Sphere = 3,
        BrightPoint = 4,
        Lense = 5,
        SphereMirror = 6,
        Polygon = 7,
    }
}
