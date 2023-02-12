using System.ComponentModel;

namespace Haiyan.Domain.Enumerations
{
    public enum BuildingElementCategory
    {
        [Description("Mineral materials, Bruk och bindemedel")]
        MineralMaterials = 1,
        [Description("Windows, doors and glass, Fönster, dörrar och glas")]
        WindowsDoorsGlass = 3,
        [Description("Paints and sealants, Färg och fog")]
        PaintsAndSealants = 4,
        [Description("Concrete, Betong")]
        Concrete = 5,
        [Description("Insulation, Isolering")]
        Insulation = 6,
        [Description("Steel and other metals, Stål och andra metaller")]
        SteelAndOtherMetals = 7,
        [Description("Blocks and tiles, murbruk och tegel")]
        BlocksAndTiles = 8,
        [Description("Building boards, Byggskivor")]
        BuildingBoards = 9,
        [Description("Waterproofing, Tätskikt")]
        Waterproofing = 10,
        [Description("Solid woods, Trävaror")]
        SolidWoods = 11,
    }
}
