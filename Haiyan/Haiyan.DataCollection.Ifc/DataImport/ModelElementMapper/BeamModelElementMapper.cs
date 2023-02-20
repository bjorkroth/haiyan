﻿using Haiyan.DataCollection.Ifc.DataImport.Materials;
using Haiyan.DataCollection.Ifc.Extensions;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.SharedBldgElements;

namespace Haiyan.DataCollection.Ifc.DataImport.ModelElementMapper
{
    public class BeamModelElementMapper : IModelElementMapper
    {

        private readonly IfcStore _model;
        private readonly IMaterialBuilder _materialBuilder;

        public BeamModelElementMapper(IfcStore model, IMaterialBuilder materialBuilder)
        {
            _model = model;
            _materialBuilder = materialBuilder;
        }

        public bool CanApply(IIfcProduct product)
        {
            if (product.ShouldBeIgnored())
                return false;

            return product.GetType().Name == nameof(IfcBeam);
        }

        public HaiyanBuildingElement MapBuildingElement(IIfcProduct product)
        {
            return new HaiyanBeam
            {
                Name = product.Name ?? "",
                Description = product.Description ?? "",
                IfcGuid = product.GlobalId.ToString(),
                Guid = Guid.NewGuid().ToString(),
                Type = product.ObjectType.ToString() ?? "",
                Geometry = GeometryParser.Parse(product.EntityLabel, _model),
                Material = _materialBuilder.Build(product, _model)
            };
        }
    }
}
