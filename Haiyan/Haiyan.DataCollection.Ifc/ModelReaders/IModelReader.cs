using Haiyan.Domain.BuildingElements;

namespace Haiyan.DataCollection.Ifc.ModelReaders
{
    public interface IModelReader
    {
        IEnumerable<HaiyanBuildingElement> Read(string filePath);
    }
}
