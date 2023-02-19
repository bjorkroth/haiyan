using Xbim.Common.Geometry;

namespace Haiyan.DataCollection.Ifc.Calculations.Geometry
{
    public static class MeshAreaCalculator
    {
        public static double CalculateAreaOfMesh(XbimShapeTriangulation mesh, XbimFaceTriangulation faceTriangulation)
        {
            var area = 0.0;
            for (int i = 0; i < faceTriangulation.Indices.Count(); i += 3)
            {
                var pointIn3d1 = mesh.Vertices[faceTriangulation.Indices[i]];
                var pointIn3d2 = mesh.Vertices[faceTriangulation.Indices[i + 1]];
                var pointIn3d3 = mesh.Vertices[faceTriangulation.Indices[i + 2]];

                var point1 = new double[3] { pointIn3d1.X, pointIn3d1.Y, pointIn3d1.Z };
                var point2 = new double[3] { pointIn3d2.X, pointIn3d2.Y, pointIn3d2.Z };
                var point3 = new double[3] { pointIn3d3.X, pointIn3d3.Y, pointIn3d3.Z };

                var pointToPointCalculator = new CalculateDistanceBetweenTwoPoints();
                var distanceFromPoint1ToPoint2 = pointToPointCalculator.CalculateDistance(point1, point2);
                var distanceFromPoint2ToPoint3 = pointToPointCalculator.CalculateDistance(point2, point3);
                var distanceFromPoint3ToPoint1 = pointToPointCalculator.CalculateDistance(point3, point1);

                double s = (distanceFromPoint1ToPoint2 + distanceFromPoint2ToPoint3 + distanceFromPoint3ToPoint1) / 2;

                area += Math.Sqrt(s * (s - distanceFromPoint1ToPoint2) * (s - distanceFromPoint2ToPoint3) * (s - distanceFromPoint3ToPoint1));
            }

            return area;
        }
    }
}
