using Haiyan.Domain.Geometry;
using System;
using System.IO;
using System.Linq;
using Xbim.Common.Geometry;
using Xbim.Common.XbimExtensions;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class GeometryParser
    {
        public static HaiyanGeometry Parse(IIfcProduct product, IfcStore model)
        {
            var haiyanGeometry = new HaiyanGeometry();

            using (var geometryStore = model.GeometryStore.BeginRead())
            {
                var instances = geometryStore.ShapeInstancesOfEntity(product.EntityLabel);
                var geometries = instances.Select(i => geometryStore.ShapeGeometryOfInstance(i) as IXbimShapeGeometryData);
                foreach (var geometry in geometries)
                {
                    using (var memoryStream = new MemoryStream(geometry.ShapeData))
                    using (var binaryReader = new BinaryReader(memoryStream))
                    {
                        var triangulation = binaryReader.ReadShapeTriangulation();
                    }

                    var geometryData = geometry as XbimShapeGeometry;

                    haiyanGeometry.Volume = Math.Floor(geometryData.BoundingBox.Volume) / 1000000000;
                    haiyanGeometry.Depth = Math.Floor(geometryData.BoundingBox.SizeX);
                    haiyanGeometry.Width = Math.Floor(geometryData.BoundingBox.SizeY);
                    haiyanGeometry.Height = Math.Floor(geometryData.BoundingBox.SizeZ);
                }
            }

            return haiyanGeometry;
        }
    }
}
