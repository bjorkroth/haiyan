using Haiyan.ConsoleApp.Calculations;
using Haiyan.Domain.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xbim.Common.Geometry;
using Xbim.Common.XbimExtensions;
using Xbim.Ifc;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class GeometryParser
    {
        public static HaiyanGeometry Parse(int entityLabel, IfcStore model)
        {
            var haiyanGeometry = new HaiyanGeometry();

            using (var geometryStore = model.GeometryStore.BeginRead())
            {
                var instances = geometryStore.ShapeInstancesOfEntity(entityLabel);
                var shapeInstances = instances.Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();

                //var geometries = instances.Select(i => geometryStore.ShapeGeometryOfInstance(i) as IXbimShapeGeometryData);

                var vol = 0.0;
                var area = 0.0;

                foreach (var shapeInstance in shapeInstances)
                {
                    var geometry = geometryStore.ShapeGeometry(shapeInstance.ShapeGeometryLabel);

                    using (var memoryStream = new MemoryStream(((IXbimShapeGeometryData)geometry).ShapeData))
                    using (var binaryReader = new BinaryReader(memoryStream))
                    {
                        var triangulation = binaryReader.ReadShapeTriangulation();

                        var mesh = triangulation.Transform(((XbimShapeInstance)shapeInstance).Transformation);
                        List<int> trs = new List<int>();
                        foreach (var f in mesh.Faces)
                        {
                            trs = trs.Concat(f.Indices).ToList();
                            area += GeometryCalculator.GetAreaOfMesh(mesh, f);
                        }
                        vol += GeometryCalculator.VolumeOfMesh(mesh.Vertices, trs);
                    }
                }


                haiyanGeometry.Volume = vol / 1000000000;

                var productGeometry = geometryStore.ShapeGeometry(entityLabel);

                if (productGeometry == null)
                {
                    return haiyanGeometry;
                }

                haiyanGeometry.Depth = Math.Floor(productGeometry.BoundingBox.SizeX);
                haiyanGeometry.Width = Math.Floor(productGeometry.BoundingBox.SizeY);
                haiyanGeometry.Height = Math.Floor(productGeometry.BoundingBox.SizeZ);

                if(haiyanGeometry.Volume == 0)
                {
                    haiyanGeometry.Volume = Math.Floor(productGeometry.BoundingBox.Volume) / 1000000000;
                }

                return haiyanGeometry;
            }
        }
    }
}
