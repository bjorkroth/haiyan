using Haiyan.ConsoleApp.Calculations;
using Haiyan.ConsoleApp.DataImport;
using Haiyan.Domain.BuildingElements;
using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.ModelGeometry.Scene;


namespace Haiyan.ConsoleApp
{
    class Program
    {
        public List<string> PropertySets = new List<string>();

        static void Main(string[] args)
        {
            //using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\V-57-V-50302030400-QTO.ifc"))
            //using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\V-57-V-70022000.ifc"))
            //using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\KP-23-V-70022000.ifc"))
            using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\K-20-V-70025000.ifc"))
            //using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\K-20-V-70098000.ifc"))
            //using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\FS-K-20-V-7000.ifc"))
            //using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\K-20-V-10060000.ifc"))
            {

                var context = new Xbim3DModelContext(model);
                context.CreateContext();

                var categories = model.Instances
                    .Select(x => x.GetType().ToString())
                    .Distinct()
                    .ToList();

                var walls = new ModelInstanceQuery(model).OfType<IIfcWall>();
                var mappedWalls = MapToHaiyanCategory.Map(walls, model);

                var columns = new ModelInstanceQuery(model).OfType<IIfcColumn>();
                var mappedColumns = MapToHaiyanCategory.Map(columns, model);

                var slabs = new ModelInstanceQuery(model).OfType<IIfcSlab>();
                var mappedSlabs = MapToHaiyanCategory.Map(slabs, model);

                var beams = new ModelInstanceQuery(model).OfType<IIfcBeam>();
                var mappedBeams = MapToHaiyanCategory.Map(beams, model);

                var roofs = new ModelInstanceQuery(model).OfType<IIfcRoof>();
                var mappedRoofs = MapToHaiyanCategory.Map(roofs, model);

                var proxy = new ModelInstanceQuery(model).OfType<IIfcBuildingElementProxy>();
                var mappedProxy = MapToHaiyanCategory.Map(proxy, model);


                var totalVolume = 0.0;

                var combinedUndefined = new List<HaiyanBuildingElement>();
                combinedUndefined.AddRange(mappedWalls.Where(x => x.Material.Layers.Any(y => y.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified)));
                combinedUndefined.AddRange(mappedColumns.Where(x => x.Material.Layers.Any()).Where(x => x.Material.Layers.Any(y => y.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified)));
                combinedUndefined.AddRange(mappedSlabs.Where(x => x.Material.Layers.Any()).Where(x => x.Material.Layers.Any(y => y.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified)));
                combinedUndefined.AddRange(mappedBeams.Where(x => x.Material.Layers.Any()).Where(x => x.Material.Layers.Any(y => y.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified)));
                combinedUndefined.AddRange(mappedRoofs.Where(x => x.Material.Layers.Any()).Where(x => x.Material.Layers.Any(y => y.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified)));
                combinedUndefined.AddRange(mappedProxy.Where(x => x.Material.Layers.Any()).Where(x => x.Material.Layers.Any(y => y.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified)));

                var namesUndefinedCategory = combinedUndefined.Select(x => x.Type).Distinct().ToList();

                var combinedNoGeometry = new List<HaiyanBuildingElement>();
                combinedNoGeometry.AddRange(BuildingElementsWithNoLayerGeometry.Get(mappedWalls));
                combinedNoGeometry.AddRange(BuildingElementsWithNoLayerGeometry.Get(mappedColumns));
                combinedNoGeometry.AddRange(BuildingElementsWithNoLayerGeometry.Get(mappedSlabs));
                combinedNoGeometry.AddRange(BuildingElementsWithNoLayerGeometry.Get(mappedBeams));
                combinedNoGeometry.AddRange(BuildingElementsWithNoLayerGeometry.Get(mappedRoofs));
                combinedNoGeometry.AddRange(BuildingElementsWithNoLayerGeometry.Get(mappedProxy));

                var namesUndefinedGeometry = combinedNoGeometry.Select(x => x.Type).Distinct().ToList();

                var weightConcreteWalls = SumWeightByBuildingElementCategory.Sum(mappedWalls, Domain.Enumerations.BuildingElementCategory.Concrete);
                var weightWoodWalls = SumWeightByBuildingElementCategory.Sum(mappedWalls, Domain.Enumerations.BuildingElementCategory.SolidWoods);
                var weightGlassWalls = SumWeightByBuildingElementCategory.Sum(mappedWalls, Domain.Enumerations.BuildingElementCategory.WindowsDoorsGlass);
                var weightWoodBeams = SumWeightByBuildingElementCategory.Sum(mappedBeams, Domain.Enumerations.BuildingElementCategory.SolidWoods);

                var weightConcreteSlabs = SumWeightByBuildingElementCategory.Sum(mappedSlabs, Domain.Enumerations.BuildingElementCategory.Concrete);

                Console.WriteLine("Total weight of Concrete walls is " + weightConcreteWalls + " kg");
                Console.WriteLine("Total weight of Wood beams is " + weightWoodBeams + " kg");
                Console.WriteLine("Total weight of glass walls is " + weightGlassWalls + " kg");
                Console.WriteLine("Total weight of Wood walls is " + weightWoodWalls + " kg");
                Console.WriteLine("Total weight of Concrete slabs is " + weightConcreteSlabs + " kg");

            }
        }
    }
}