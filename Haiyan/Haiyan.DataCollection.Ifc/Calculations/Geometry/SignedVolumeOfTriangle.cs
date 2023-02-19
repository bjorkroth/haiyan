using Xbim.Common.Geometry;

namespace Haiyan.DataCollection.Ifc.Calculations.Geometry
{
    public static class SignedVolumeOfTriangle
    {
        //Calculation of Signed volume of triangle from http://chenlab.ece.cornell.edu/Publication/Cha/icip01_Cha.pdf
        //https://stackoverflow.com/questions/1406029/how-to-calculate-the-volume-of-a-3d-mesh-object-the-surface-of-which-is-made-up
        public static double CalculateSignedVolumeOfTriangle(XbimPoint3D point1, XbimPoint3D point2, XbimPoint3D point3)
        {
            var vectorFromPoint321 = point3.X * point2.Y * point1.Z;
            var vectorFromPoint231 = point2.X * point3.Y * point1.Z;
            var vectorFromPoint312 = point3.X * point1.Y * point2.Z;
            var vectorFromPoint132 = point1.X * point3.Y * point2.Z;
            var vectorFromPoint213 = point2.X * point1.Y * point3.Z;
            var vectorFromPoint123 = point1.X * point2.Y * point3.Z;
            return 1.0 / 6.0 * (-vectorFromPoint321 + vectorFromPoint231 + vectorFromPoint312 - vectorFromPoint132 - vectorFromPoint213 + vectorFromPoint123);
        }
    }
}
