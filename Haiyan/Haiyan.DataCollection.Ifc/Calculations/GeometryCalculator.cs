using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common.Geometry;

namespace Haiyan.DataCollection.Ifc.Calculations
{
    public static class GeometryCalculator
    {
        public static double CalulateVolumeOfMesh(IList<XbimPoint3D> vertices, List<int> trs)
        {
            double volume = 0.0;

            for (int i = 0; i < trs.Count; i += 3)
            {
                XbimPoint3D pointIn3d1 = vertices[trs[i + 0]];
                XbimPoint3D pointIn3d2 = vertices[trs[i + 1]];
                XbimPoint3D pointIn3d3 = vertices[trs[i + 2]];

                var vol = SignedVolumeOfTriangle(pointIn3d1, pointIn3d2, pointIn3d3);

                volume += vol;
            }
            return Math.Abs(volume);
        }

        public static double CalculateFacesVolume(XbimRect3D boundingBox)
        {
            var boundingBoxVolume = boundingBox.Volume;

            var widthOfVoid = boundingBox.SizeY - 1;
            var halfWidth = widthOfVoid / 2;
            var areaOfBottom = Math.Pow(halfWidth, 2) * Math.PI;
            var volumeOfVoid = areaOfBottom * boundingBox.SizeX;

            var remainingVolume = boundingBoxVolume - volumeOfVoid;

            return remainingVolume;
        }

        public static double GetAreaOfMesh(XbimShapeTriangulation mesh, XbimFaceTriangulation faceTriangulation)
        {
            var area = 0.0;
            for (int i = 0; i < faceTriangulation.Indices.Count(); i += 3)
            {
                var pointIn3d1 = mesh.Vertices[faceTriangulation.Indices[i]];
                var pointIn3d2 = mesh.Vertices[faceTriangulation.Indices[i + 1]];
                var pointIn3d3 = mesh.Vertices[faceTriangulation.Indices[i + 2]];
                var t = new XbimPoint3D[] { pointIn3d1, pointIn3d2, pointIn3d3 };

                double[] point1 = new double[3] { pointIn3d1.X, pointIn3d1.Y, pointIn3d1.Z };
                double[] point2 = new double[3] { pointIn3d2.X, pointIn3d2.Y, pointIn3d2.Z };
                double[] point3 = new double[3] { pointIn3d3.X, pointIn3d3.Y, pointIn3d3.Z };
                var distp1p2 = Math.Sqrt(point1.Zip(point2, (a, b) => (a - b) * (a - b)).Sum());
                var distp2p3 = Math.Sqrt(point2.Zip(point3, (a, b) => (a - b) * (a - b)).Sum());
                var distp3p1 = Math.Sqrt(point3.Zip(point1, (a, b) => (a - b) * (a - b)).Sum());
                double s = (distp1p2 + distp2p3 + distp3p1) / 2;
                area += Math.Sqrt(s * (s - distp1p2) * (s - distp2p3) * (s - distp3p1));
            }
            //Console.WriteLine("Area Face:{0}", area);
            return area;
        }

        public static double SignedVolumeOfTriangle(XbimPoint3D point1, XbimPoint3D point2, XbimPoint3D point3)
        {
            var v321 = point3.X * point2.Y * point1.Z;
            var v231 = point2.X * point3.Y * point1.Z;
            var v312 = point3.X * point1.Y * point2.Z;
            var v132 = point1.X * point3.Y * point2.Z;
            var v213 = point2.X * point1.Y * point3.Z;
            var v123 = point1.X * point2.Y * point3.Z;
            return 1.0 / 6.0 * (-v321 + v231 + v312 - v132 - v213 + v123);
        }
    }
}