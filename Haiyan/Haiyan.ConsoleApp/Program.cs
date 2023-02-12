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
            using (var model = IfcStore.Open(@"C:\Files\Haiyan_IFC\KP-23-V-70022000.ifc"))
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
                var columns = new ModelInstanceQuery(model).OfType<IIfcColumn>();
                var slabs = new ModelInstanceQuery(model).OfType<IIfcSlab>();
                var beams = new ModelInstanceQuery(model).OfType<IIfcBeam>();

                var mappedWalls = MapToHaiyanCategory.Map(walls);
                var mappedColumns = MapToHaiyanCategory.Map(columns);
                var mappedSlabs = MapToHaiyanCategory.Map(slabs);
                var mapsBeams = MapToHaiyanCategory.Map(beams);

                var totalVolume = 0.0;

                foreach (var item in ducts)
                {
                    //GetVolume(item);
                    //var value = GetProperty(item, "Volume");
                    
                    var volume = CalculationHelper.CalculateVolume(item, context);
                    Console.WriteLine("Volume for item " + item.Name.Value + " is " + volume);

                    totalVolume += volume;
                }
                
                Console.WriteLine("Total volume of duct faces is " + totalVolume + " m3");
            }
        }
    }
}