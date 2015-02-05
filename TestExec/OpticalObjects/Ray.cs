using System;
using System.Collections.Generic;
 
using System.Text;
using System.Windows.Forms;
using OpticalBuilderLib.Configuration;
using OpticalBuilderLib.MathLib;
using System.Drawing;
using OpticalBuilderLib.TypeExtentions;

namespace OpticalBuilderLib.OpticalObjects
{
    public class Ray : ObjectProto
    {
        protected Angle angle;
        public bool swap_density = false;
        private List<Line> LineArray;
        private DoubleExtention current_density;
        private bool FinalizedLoad = false;
        private string bound_point_name;
        public string BoundPointName
        {
            get { return bound_point_name; }
            set
            {
                if(!ObjectCollection.Instance.Allow_List_Access)
                    if(ObjectCollection.Instance.ObjectExists(value))
                        coordinates = ObjectCollection.Instance.GetObjectByName(value).Coordinates;
                    else
                        ObjectCollection.Instance.AddObject(new BrightPoint(GenName(GetSpec(ObjectTypes.BrightPoint)), coordinates));
                bound_point_name = value;
            }
        }
        public BrightPoint BoundPoint
        {
            get { return ((BrightPoint)ObjectCollection.Instance.GetObjectByName(bound_point_name)); }
        }
        public DoubleExtention Current_density
        {
            get { return current_density; }
        }
        public Angle Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                if(ObjectCollection.Instance.RayExists(name))
                {
                    ObjectsChanged(this, new EventArgs());
                }
                RaiseChanged();
            }
        }
        public override int ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                if ((ObjectCollection.Instance.RayMaxID == value) || (ObjectCollection.Instance.Allow_List_Access == true))
                    id = value;
            }
        }
        public Ray(string Name, int ID, SystemCoordinates Beginning, Angle Angle, string PointName, bool GenNewPoint = false)
        {
            LineArray = new List<Line>();
            name = Name;
            id = ID;
            coordinates = Beginning;
            angle = Angle;
            current_density = ObjectCollection.Instance.VneshDensity;
            bound_point_name = PointName;
            if (GenNewPoint)
            {
                PointName = GenName(GetSpec(ObjectTypes.BrightPoint));
                ObjectCollection.Instance.AddObject(new BrightPoint(PointName,coordinates));
            }
            bound_point_name = PointName;
            ((BrightPoint) ObjectCollection.Instance.GetObjectByName(bound_point_name)).Removal += PointRemove;
            ((BrightPoint) ObjectCollection.Instance.GetObjectByName(bound_point_name)).NameChange += NameChange;
            FinalizedLoad = true;
        }
        public Ray(string SaveString)
        {
            string[] splitted = SaveString.Split('^');
            if (splitted.Length != 7)
            {
                throw new OpticalBuilderLib.Exceptions.ErrorWhileReading();
            }
            try
            {
                name = splitted[1];
                id = Convert.ToInt32(splitted[2]);
                coordinates = new SystemCoordinates(Convert.ToDouble(splitted[3]),Convert.ToDouble(splitted[4]));
                angle = Convert.ToDouble(splitted[5]);
                LineArray = new List<Line>();
                current_density = ObjectCollection.Instance.VneshDensity; //fix_when_system_is_ready
                bound_point_name = splitted[6];
            }
            catch
            {
                throw new OpticalBuilderLib.Exceptions.ErrorWhileReading();
            }

        }
        public void ObjectsChanged(Object sender, EventArgs e)
        {
            
            if(!FinalizedLoad)
            {
                if(ObjectCollection.Instance.ObjectExists(bound_point_name))
                    coordinates = ObjectCollection.Instance.GetObjectByName(bound_point_name).Coordinates;
                else
                    ObjectCollection.Instance.AddObject(new BrightPoint(bound_point_name, coordinates));
                BoundPoint.Removal += PointRemove;
                BoundPoint.NameChange += NameChange;
                FinalizedLoad = true;
            }
            try
            {
                coordinates = BoundPoint.Coordinates;
            }
            catch (Exception)
            {
                if(ObjectCollection.Instance.ObjectExists(bound_point_name))
                    coordinates = ObjectCollection.Instance.GetObjectByName(bound_point_name).Coordinates;
                else
                {
                    ObjectCollection.Instance.AddObject(new BrightPoint(bound_point_name, coordinates));
                    BoundPoint.Removal += PointRemove;
                    BoundPoint.NameChange += NameChange;
                }
                coordinates = BoundPoint.Coordinates;
            }
            
            LineArray = new List<Line>(); 
            LineArray.Add(new Line(coordinates, angle));
            LineArray[0].FirstEnd = coordinates;
            LineArray[0].AngleForOnePointLines = angle;
            bool got = false;
            bool GotAngle;
            SystemCoordinates LST = SystemCoordinates.Zero;
            Angle LSTa = new Angle(0);
            int refl_limit = Convert.ToInt32(SConfig.C["Ray.ReflectionLimit"]);
            current_density = ObjectCollection.Instance.VneshDensity;
            DoubleExtention new_density = current_density;
            bool set_dens = false;
            for(int i = 1; i<refl_limit+1; i++)
            {
                got = false;
                GotAngle = false;
                foreach (ObjectProto X in ObjectCollection.Instance.objects)
                {
                        bool PF;
                        bool AG;
                        Angle anglee;
                        
                        SystemCoordinates x = X.IntersectWithLine(out PF, LineArray[i - 1], out AG, out anglee, this);
                        if(PF && AG && got)
                        {
                            if(x.Distance(LineArray[i-1].FirstEnd) < LST.Distance(LineArray[i-1].FirstEnd))
                            {
                                LST = x;
                                LSTa = anglee;
                                GotAngle = true;
                                if (X is IPreloml && swap_density)
                                {
                                    if (current_density != ((IPreloml)X).OpticalDensity) new_density = ((IPreloml)X).OpticalDensity;
                                    else new_density = ObjectCollection.Instance.VneshDensity;
                                    swap_density = false;
                                    set_dens = true;
                                }
                                else set_dens = false;
                            }
                            swap_density = false;
                        }
                        if (PF && AG && !got)
                        {
                            LST = x;
                            LSTa = anglee;
                            got = true;
                            GotAngle = true;
                            if (X is IPreloml && swap_density)
                            {
                                if (current_density != ((IPreloml)X).OpticalDensity) new_density = ((IPreloml)X).OpticalDensity;
                                else new_density = ObjectCollection.Instance.VneshDensity;
                                swap_density = false;
                                set_dens = true;
                            }
                            else set_dens = false;
                            swap_density = false;
                        }
                        if (PF && !AG && got)
                        {
                            if (x.Distance(LineArray[i - 1].FirstEnd) < LST.Distance(LineArray[i - 1].FirstEnd))
                            {
                                LST = x;
                                GotAngle = false;
                                if (X is IPreloml && swap_density)
                                {
                                    if (current_density != ((IPreloml)X).OpticalDensity) new_density = ((IPreloml)X).OpticalDensity;
                                    else new_density = ObjectCollection.Instance.VneshDensity;
                                    swap_density = false;
                                    set_dens = true;
                                }
                                else set_dens = false;
                            }
                            swap_density = false;
                        }
                        if (PF && !AG && !got)
                        {
                            LST = x;
                            got = true;
                            GotAngle = false;
                            if (X is IPreloml && swap_density)
                            {
                                if (current_density != ((IPreloml)X).OpticalDensity) new_density = ((IPreloml)X).OpticalDensity;
                                else new_density = ObjectCollection.Instance.VneshDensity;
                                swap_density = false;
                                set_dens = true;
                            }
                            else set_dens = false;
                            swap_density = false;
                        }
                        swap_density = false;
                }
                if (got)
                {
                    LineArray[i - 1].SecondEnd = LST;
                    if(set_dens) current_density = new_density;
                    if (i != refl_limit)
                    {
                        
                        if (GotAngle == true)
                            LineArray.Add(new Line(LST, LSTa));
                        else break;
                    }
                    swap_density = false;
                }
                else
                {
                    swap_density = false;
                    break;
                }
            }
        }
        public override string GenerateSaveString()
        {
            string SaveStr = GetTypeSpecifier() + "^" + Name + "^" + ID.ToString()+"^"+coordinates.X.ToString()+"^"+coordinates.Y.ToString()+"^"+angle.ToString()+"^"+bound_point_name;
            return SaveStr;
        }
        public DoubleExtention DistanceToPoint(Point X)
        {
            if(LineArray.Count > 0)
            {
                DoubleExtention a = LineArray[0].DistanceToPoint(X);
                foreach (var line in LineArray)
                    a = Math.Min(a,line.DistanceToPoint(X));
                return a;
            }
            else
                return -1;
        }
        public DoubleExtention DistanceToPoint(SystemCoordinates X)
        {
            if (LineArray.Count > 0)
            {
                DoubleExtention a = LineArray[0].DistanceToPoint(X);
                foreach (var line in LineArray)
                    a = Math.Min(a, line.DistanceToPoint(X));
                return a;
            }
            else
                return -1;
        }
        public override int DistanceToPointS(Point X)
        {
            if (LineArray.Count > 0)
            {
                DoubleExtention a = LineArray[0].DistanceToPoint(X);
                foreach (var line in LineArray)
                    a = Math.Min(a, line.DistanceToPoint(X));
                return (int)Math.Ceiling(a*CoordinateSystem.Instance.Scale);
            }
            else
                return -1;
        }
        public int DistanceToPointS(SystemCoordinates X)
        {
            if (LineArray.Count > 0)
            {
                DoubleExtention a = LineArray[0].DistanceToPoint(X);
                foreach (var line in LineArray)
                    a = Math.Min(a, line.DistanceToPoint(X));
                return (int)Math.Ceiling(a * CoordinateSystem.Instance.Scale);
            }
            else
                return -1;
        }
        public override void Drawer()
        {
            foreach (var x in LineArray)
            {
                Drawing.LineDrawer.RayAutoSelect(x, System.Drawing.Color.DarkGreen, 1, Selected);
            }
        }
        public override SystemCoordinates IntersectWithLine(out bool PointFound, Line ToIntersect, out bool angle_got, out Angle anglee, Ray ray)
        {
            throw new Exceptions.NotImplementedYet();
        }
        public override string GetTypeSpecifier()
        {
            return GetSpec(1);
        }
        void NameChange(object sender, EventArgs e)
        {
            bound_point_name = ((BrightPoint) sender).Name;
        }
        void PointRemove(object sender, EventArgs e)
        {
            if(((ObjectProto)sender).Name == bound_point_name)
                ObjectCollection.Instance.DeleteObjectRay(this);
        }
        public override void BeingRemoved()
        {
            BoundPoint.Removal -= PointRemove;
            BoundPoint.NameChange -= NameChange;
            base.BeingRemoved();
        }
        public void ChangeBoundPoint(BrightPoint newPoint)
        {
            if (newPoint.Name != bound_point_name)
            {
                BoundPoint.Removal -= PointRemove;
                bound_point_name = newPoint.Name;
                BoundPoint.Removal += PointRemove;
            }
        }
        public override void Rotate(SystemCoordinates to, SystemCoordinates from)
        {
            Line aa = new Line(coordinates, to, false);
            aa.BuildAngle();
            Line bb = new Line(coordinates, from, false);
            bb.BuildAngle();
            Angle c = new Angle(aa.AngleForOnePointLines.GetInDegrees()-bb.AngleForOnePointLines.GetInDegrees(), false);
            angle = new Angle(angle.GetInDegrees() + c.GetInDegrees(), false);
            base.Rotate(to, from);
        }
        public override void MoveTo(SystemCoordinates to, SystemCoordinates from)
        {
            Rotate(to, from);
        }
    }
}
