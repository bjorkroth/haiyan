using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xbim.Common.Geometry;
using Xbim.Common.XbimExtensions;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.ModelGeometry.Scene;


namespace Haiyan.ConsoleApp
{
    class Program
    {
        public List<string> PropertySets = new List<string>();

        static void Main(string[] args)
        {
            using (var model = IfcStore.Open(@"C:\Files\V-57-V-50302030400-QTO.ifc"))
            {

                var context = new Xbim3DModelContext(model);
                context.CreateContext();
                
                //Get all spaces in the model. 
                //We use ToList() here to avoid multiple enumeration with Count() and foreach(){}
                var ducts = model.Instances.OfType<IIfcFlowSegment>().ToList();

                var totalVolume = 0.0;

                foreach (var item in ducts)
                {
                    //GetVolume(item);
                    //var value = GetProperty(item, "Volume");
                    
                    var volume = CalculateVolume(item, context);
                    Console.WriteLine("Volume for item " + item.Name.Value + " is " + volume);

                    totalVolume += volume;
                }
                
                Console.WriteLine("Total volume of duct faces is " + totalVolume + " m3");
            }
        }

        private static IIfcValue GetVolume(IIfcProduct product)
        {
            var volume = product.IsDefinedBy
                .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                .OfType<IIfcElementQuantity>()
                .SelectMany(qset => qset.Quantities)
                .OfType<IIfcQuantityVolume>()
                .FirstOrDefault() != null
                ? product.IsDefinedBy
                    .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                    .OfType<IIfcElementQuantity>()
                    .SelectMany(qset => qset.Quantities)
                    .OfType<IIfcQuantityVolume>()
                    .FirstOrDefault()!.VolumeValue
                : 0;

            if (volume != 0)
                return volume;
            return GetProperty(product, "Volume");
        }

        private static IIfcValue GetProperty(IIfcProduct product, string name)
        {
            var propertySetsWithValues = product.IsDefinedBy
                .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                .OfType<IIfcPropertySet>()
                .SelectMany(pset => pset.HasProperties);
            
            var propertiesWithSingleValue = propertySetsWithValues.OfType<IIfcPropertySingleValue>();

            return propertiesWithSingleValue
                .FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase) ||
                                     p.Name.ToString().ToLower().Contains(name.ToLower()))!.NominalValue;
        }

        private static double GetBottomArea()
        {
            return 0;
        }
        
        private static double GetAreaOfMesh(XbimShapeTriangulation mesh, XbimFaceTriangulation f)
        {
            var area = 0.0;
            for (int i = 0; i < f.Indices.Count(); i += 3)
            {
                var p1 = mesh.Vertices[f.Indices[i]];
                var p2 = mesh.Vertices[f.Indices[i + 1]];
                var p3 = mesh.Vertices[f.Indices[i + 2]];
                var t = new XbimPoint3D[] { p1, p2, p3 };

                double[] point1 = new double[3] { p1.X, p1.Y, p1.Z };
                double[] point2 = new double[3] { p2.X, p2.Y, p2.Z };
                double[] point3 = new double[3] { p3.X, p3.Y, p3.Z };
                var distp1p2 = Math.Sqrt(point1.Zip(point2, (a, b) => (a - b) * (a - b)).Sum());
                var distp2p3 = Math.Sqrt(point2.Zip(point3, (a, b) => (a - b) * (a - b)).Sum());
                var distp3p1 = Math.Sqrt(point3.Zip(point1, (a, b) => (a - b) * (a - b)).Sum());
                double s = (distp1p2 + distp2p3 + distp3p1) / 2;
                area += Math.Sqrt(s * (s - distp1p2) * (s - distp2p3) * (s - distp3p1));
            }
            //Console.WriteLine("Area Face:{0}", area);
            return area;
        }
        
        
        private static double SignedVolumeOfTriangle(XbimPoint3D p1, XbimPoint3D p2, XbimPoint3D p3)
        {
            var v321 = p3.X * p2.Y * p1.Z;
            var v231 = p2.X * p3.Y * p1.Z;
            var v312 = p3.X * p1.Y * p2.Z;
            var v132 = p1.X * p3.Y * p2.Z;
            var v213 = p2.X * p1.Y * p3.Z;
            var v123 = p1.X * p2.Y * p3.Z;
            return (1.0 / 6.0) * (-v321 + v231 + v312 - v132 - v213 + v123);
        }
        
        public static double VolumeOfMesh(IList<XbimPoint3D> vertices, List<int> trs)
        {
            double volume = 0.0;

            for (int i = 0; i < trs.Count; i += 3)
            {
                XbimPoint3D p1 = vertices[trs[i + 0]];
                XbimPoint3D p2 = vertices[trs[i + 1]];
                XbimPoint3D p3 = vertices[trs[i + 2]];

                var vol = SignedVolumeOfTriangle(p1, p2, p3);
                //Console.WriteLine("Volume Trg:{0}", vol);

                volume += vol;
            }
            return Math.Abs(volume);
        }

        private static double FacesVolume(XbimRect3D boundingBox)
        {
            var boundingBoxVolume = boundingBox.Volume;

            var widthOfVoid = boundingBox.SizeY - 1;
            var halfWidth = widthOfVoid / 2;
            var areaOfBottom = Math.Pow(halfWidth,2) * Math.PI;
            var volumeOfVoid = areaOfBottom * boundingBox.SizeX;

            var remainingVolume = boundingBoxVolume - volumeOfVoid;

            return remainingVolume;
        }

        private static double CalculateVolume(IIfcProduct product, Xbim3DModelContext context)
        {
            var productShape = context.ShapeInstancesOf(product);
            var _productShape = productShape.Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();
            var geometry = new XbimShapeGeometry();

            XbimShapeTriangulation mesh;

            var vol = 0.0;
            var area = 0.0;

            foreach (var shapeInstance in _productShape)
            {
                geometry = context.ShapeGeometry(shapeInstance);

                // var facesVolume = FacesVolume(geometry.BoundingBox);
                //
                // var ms = new MemoryStream(((IXbimShapeGeometryData)geometry).ShapeData);
                // var br = new BinaryReader(ms);
                // mesh = br.ReadShapeTriangulation();
                // mesh = mesh.Transform(((XbimShapeInstance)shapeInstance).Transformation);
                //
                // List<int> trs = new List<int>();
                // foreach (var f in mesh.Faces)
                // {
                //     trs = trs.Concat(f.Indices).ToList();
                //     area += GetAreaOfMesh(mesh, f);
                // }
                // vol += VolumeOfMesh(mesh.Vertices, trs);

                vol += FacesVolume(geometry.BoundingBox);
            }

            return Math.Abs(vol * 1e-9);
        }
    }
}