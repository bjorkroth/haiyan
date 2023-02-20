using Haiyan.Domain.BuildingElements;

namespace Haiyan.DataCollection.Ifc.ModelReaders
{
    public interface IModelReader
    {
        List<HaiyanBuildingElement> Read(string filePath);
    }
}
