﻿using Haiyan.Domain.BuildingElements;
using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public class ModelInstanceQuery
    {
        private IfcStore _model;

        public ModelInstanceQuery(IfcStore model) {
            _model = model;
        }

        public List<T> OfType<T>() where T : IIfcObject
        {
            var buildingElements = new List<HaiyanBuildingElement>();

            var type = typeof(T);
            var objects = _model.Instances.OfType<T>().ToList();

            return objects;
        }
    }
}
