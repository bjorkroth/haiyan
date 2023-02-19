using Xbim.Common.Geometry;

namespace Haiyan.DataCollection.Ifc.Calculations.Geometry
{
    public static class FacesVolumeCalculator
    {
        public static double CalculateVolumeOfFaces(XbimRect3D boundingBox)
        {
            var boundingBoxVolume = boundingBox.Volume;

            var widthOfVoid = boundingBox.SizeY - 1;
            var halfWidth = widthOfVoid / 2;
            var areaOfBottom = Math.Pow(halfWidth, 2) * Math.PI;
            var volumeOfVoid = areaOfBottom * boundingBox.SizeX;

            var remainingVolume = boundingBoxVolume - volumeOfVoid;

            return remainingVolume;
        }
    }
}
