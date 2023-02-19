namespace Haiyan.DataCollection.Ifc.Calculations.Geometry
{
    public class CalculateDistanceBetweenTwoPoints
    {
        public double CalculateDistance(double[] pointA, double[] pointB)
        {
            return Math.Sqrt(pointA.Zip(pointB, (a, b) => (a - b) * (a - b)).Sum());
        }
    }
}
