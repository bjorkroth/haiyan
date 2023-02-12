using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class GetProductMaterial
    {
        public static string Get(IIfcProduct product)
        {
            return product.Material.ToString();
        }
    }
}
