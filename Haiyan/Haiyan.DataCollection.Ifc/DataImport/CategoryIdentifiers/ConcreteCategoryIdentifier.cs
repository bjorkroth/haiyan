﻿using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Lists;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.SharedBldgElements;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    public class ConcreteCategoryIdentifier : ICategoryIdentifier
    {
        public bool CanApply(IIfcProduct product, string materialName)
        {
            if (string.IsNullOrEmpty(materialName))
                return false;

            if (ConcreteMappingList.MappingList.Any(materialName.Contains))
                return true;

            var productName = product.Name?.Value?.ToString()?.ToUpper();

            if (string.IsNullOrEmpty(productName))
                return false;

            if (ConcreteMappingList.MappingList.Any(x => productName.Contains(x)))
                return true;

            var productType = product.GetType().Name;
            if (productName == "W" && productType == nameof(IfcWall))
                return true;
            if (productName == "V" && productType == nameof(IfcWall))
                return true;

            return false;
        }

        public BuildingElementCategory Category => BuildingElementCategory.Concrete;
    }
}
