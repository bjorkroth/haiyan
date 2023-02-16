using Haiyan.Domain.Materials;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class MaterialParser
    {
        public static HaiyanMaterial Parse(IIfcProduct product)
        {
            var material = new HaiyanMaterial();

            var ifcRelAssociatesMaterials = product.HasAssociations.OfType<IIfcRelAssociatesMaterial>();

            if (!ifcRelAssociatesMaterials.Any())
            {
                material.Name = "Unknown";
                material.Layers = new List<HaiyanMaterialLayer>
                {
                    new HaiyanMaterialLayer
                    {
                        Name = "Unknown",
                        BoverketProductCategory = BuildingElementCategoryParser.Parse(product, "Unknown")
                    }
                };

                return material;
            }

            var firstMaterial = ifcRelAssociatesMaterials.FirstOrDefault();

            if(ifcRelAssociatesMaterials.Count() > 1)
            {
                Console.WriteLine("Multiple materials for item " + product.Name.ToString());
            }

            var relatedMaterial = firstMaterial.RelatingMaterial as IIfcMaterial;
            var materialSetLayerUsage = firstMaterial.RelatingMaterial as IIfcMaterialLayerSetUsage;
            var materialSetLayer = firstMaterial.RelatingMaterial as IIfcMaterialLayerSet;
            var materialList = firstMaterial.RelatingMaterial as IIfcMaterialList;

            if(materialSetLayerUsage != null)
            {
                material.Layers = GetLayers(materialSetLayerUsage, product);
            }

            if (materialSetLayer != null)
            {
                material.Layers = GetLayers(materialSetLayer, product);
            }

            if (materialList != null)
            {
                material.Layers = GetLayers(materialList, product);
            }

            if (relatedMaterial == null && material.Layers.Any())
            {
                return material;
            }

            if (relatedMaterial == null)
            {
                return material;
            }

            material.Name = relatedMaterial.Name;
            
            if(material.Layers == null)
            {
                material.Layers = new List<HaiyanMaterialLayer>()
                {
                    new HaiyanMaterialLayer
                    {
                        Name = material.Name,
                        BoverketProductCategory = BuildingElementCategoryParser.Parse(product, material.Name)
                    }
                };
            }

            return material;
        }

        private static List<HaiyanMaterialLayer> GetLayers(IIfcMaterialList ifcMaterialList, IIfcProduct product)
        {
            if (ifcMaterialList == null)
            {
                return default;
            }

            if (ifcMaterialList.Materials.Any())
            {
                var layers = new List<HaiyanMaterialLayer>();

                foreach (var material in ifcMaterialList.Materials)
                {
                    layers.Add(new HaiyanMaterialLayer
                    {
                        Name = material.Name,
                        BoverketProductCategory = BuildingElementCategoryParser.Parse(product, material.Name)
                    });
                }

                return layers;
            }

            return default;
        }

        private static List<HaiyanMaterialLayer> GetLayers(IIfcMaterialLayerSetUsage ifcMaterialLayerSetUsage, IIfcProduct product)
        {
            if(ifcMaterialLayerSetUsage == null)
            {
                return default;
            }

            if (ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.Any())
            {
                return ProcessLayers(ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.ToList(), product);
            }

            return default;
        }

        private static List<HaiyanMaterialLayer> GetLayers(IIfcMaterialLayerSet ifcMaterialLayerSet, IIfcProduct product)
        {
            if (ifcMaterialLayerSet == null)
            {
                return default;
            }

            if (ifcMaterialLayerSet.MaterialLayers.Any())
            {
                return ProcessLayers(ifcMaterialLayerSet.MaterialLayers.ToList(), product);
            }

            return default;
        }

        private static List<HaiyanMaterialLayer> ProcessLayers(List<IIfcMaterialLayer> ifcMaterialLayers, IIfcProduct product)
        {
            var layers = new List<HaiyanMaterialLayer>();

            foreach (var layer in ifcMaterialLayers)
            {
                layers.Add(new HaiyanMaterialLayer
                {
                    Name = layer.Material.Name,
                    Thickness = layer.LayerThickness,
                    BoverketProductCategory = BuildingElementCategoryParser.Parse(product, layer.Material.Name)
                });
            }

            return layers;
        }
    }
}
