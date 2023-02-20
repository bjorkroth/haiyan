using Haiyan.DataCollection.Ifc.Calculations.Geometry;
using Haiyan.Domain.Geometry;
using Xbim.Common.Geometry;
using Xbim.Common.XbimExtensions;
using Xbim.Ifc;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public static class GeometryParser
    {
        public static HaiyanGeometry Parse(int entityLabel, IfcStore model)
        {
            using var geometryStore = model.GeometryStore.BeginRead();
            var instances = geometryStore.ShapeInstancesOfEntity(entityLabel);
            var shapeInstances = instances.Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();

            var volumeOfEntity = 0.0;
            var area = 0.0;

            foreach (var shapeInstance in shapeInstances)
            {
                var geometry = geometryStore.ShapeGeometry(shapeInstance.ShapeGeometryLabel);

                using var memoryStream = new MemoryStream(((IXbimShapeGeometryData)geometry).ShapeData);
                using var binaryReader = new BinaryReader(memoryStream);
                var triangulation = binaryReader.ReadShapeTriangulation();

                var mesh = triangulation.Transform(((XbimShapeInstance)shapeInstance).Transformation);
                List<int> triangles = new List<int>();
                foreach (var faceTriangulation in mesh.Faces)
                {
                    triangles = triangles.Concat(faceTriangulation.Indices).ToList();
                    area += MeshAreaCalculator.CalculateAreaOfMesh(mesh, faceTriangulation);
                }

                volumeOfEntity += MeshVolumeCalculator.CalculateVolumeOfMesh(mesh.Vertices, triangles);
            }

            var haiyanGeometry = new HaiyanGeometry();

            const int divideForCubicMillimetersToCubicMeters = 1000000000;
            haiyanGeometry.Volume = volumeOfEntity / divideForCubicMillimetersToCubicMeters;

            var productGeometry = geometryStore.ShapeGeometry(entityLabel);

            if (productGeometry == null)
            {
                return haiyanGeometry;
            }

            //TODO: Break out to separate function
            haiyanGeometry.Depth = Math.Floor(productGeometry.BoundingBox.SizeX);
            haiyanGeometry.Width = Math.Floor(productGeometry.BoundingBox.SizeY);
            haiyanGeometry.Height = Math.Floor(productGeometry.BoundingBox.SizeZ);

            if(haiyanGeometry.Volume == 0)
            {
                haiyanGeometry.Volume = Math.Floor(productGeometry.BoundingBox.Volume) / divideForCubicMillimetersToCubicMeters;
            }

            return haiyanGeometry;
        }
    }
}
