using Haiyan.ConsoleApp.Calculations;
using Haiyan.ConsoleApp.DataImport;
using Haiyan.Domain.BuildingElements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xbim.Common.Geometry;
using Xbim.Common.XbimExtensions;
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
            using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\K-20-V-70098000.ifc"))
            {

                var context = new Xbim3DModelContext(model);
                context.CreateContext();
                
                //Get all spaces in the model. 
                //We use ToList() here to avoid multiple enumeration with Count() and foreach(){}
                var ducts = model.Instances.OfType<IIfcFlowSegment>().ToList();

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
                var mappedRoofs = MapToHaiyanCategory.Map(beams, model);

                var proxy = new ModelInstanceQuery(model).OfType<IIfcBuildingElementProxy>();
                var mappedProxy = MapToHaiyanCategory.Map(proxy, model);

                var totalVolume = 0.0;

                var combinedUndefined = new List<HaiyanBuildingElement>();
                combinedUndefined.AddRange(mappedWalls.Where(x => x.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified));
                combinedUndefined.AddRange(mappedColumns.Where(x => x.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified));
                combinedUndefined.AddRange(mappedSlabs.Where(x => x.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified));
                combinedUndefined.AddRange(mappedBeams.Where(x => x.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified));
                combinedUndefined.AddRange(mappedRoofs.Where(x => x.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified));
                combinedUndefined.AddRange(mappedProxy.Where(x => x.BoverketProductCategory == Domain.Enumerations.BuildingElementCategory.Unspecified));

                var names = combinedUndefined.Select(x => x.Name).Distinct().ToList();
              
                foreach (var item in walls)
                {
                    //GetVolume(item);
                    //var value = GetProperty(item, "Volume");
                    
                    var haiyanGeometry = GeometryCalculator.CalculateVolume(item, context);
                    Console.WriteLine("Volume for item " + item.Name.Value + " is " + haiyanGeometry.Volume);

                    totalVolume += haiyanGeometry.Volume;
                }
                
                Console.WriteLine("Total volume of duct faces is " + totalVolume + " m3");
            }
        }
    }
}