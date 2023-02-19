using Xbim.Common.Geometry;

namespace Haiyan.DataCollection.Ifc.Calculations.Geometry
{
    public static class MeshVolumeCalculator
    {
        public static double CalculateVolumeOfMesh(IList<XbimPoint3D> vertices, List<int> triangles)
        {
            double volumeOfMesh = 0.0;

            for (int i = 0; i < triangles.Count; i += 3)
            {
                XbimPoint3D pointIn3d1 = vertices[triangles[i + 0]];
                XbimPoint3D pointIn3d2 = vertices[triangles[i + 1]];
                XbimPoint3D pointIn3d3 = vertices[triangles[i + 2]];

                var volumeOfTriangle = SignedVolumeOfTriangle.CalculateSignedVolumeOfTriangle(pointIn3d1, pointIn3d2, pointIn3d3);

                volumeOfMesh += volumeOfTriangle;
            }
            return Math.Abs(volumeOfMesh);
        }
    }
}
