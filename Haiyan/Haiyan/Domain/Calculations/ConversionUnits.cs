using System.ComponentModel;

namespace Haiyan.Domain.Calculations
{
    public enum ConversionUnits
    {
        [Description("kg/m³")]
        kgm2,
        [Description("kg/m²")]
        kgm3,
        [Description("kWh")]
        kWh,
        [Description("m²")]
        m2,
        [Description("MJ/Liter")]
        MJliter,
    }
}
