using Haiyan.Domain.BuildingElements;

namespace Haiyan.DataCollection.Ifc.ModelReaders
{
    public interface IModelReader : IDisposable
    {
        IEnumerable<HaiyanBuildingElement> Read(string filePath);
        void Dispose();
    }
}
