using Haiyan.DataCollection.Ifc.Calculations.Weight;
using Haiyan.Domain.Materials;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public static class MaterialParser
    {
        public static HaiyanMaterial Parse(IIfcProduct product, IfcStore model)
        {
            var material = new HaiyanMaterial();

            var ifcRelAssociatesMaterials = product.HasAssociations.OfType<IIfcRelAssociatesMaterial>();

            if (!ifcRelAssociatesMaterials.Any())
            {
                material.Name = "Unknown";
                var layer = new HaiyanMaterialLayer
                {
                    Name = "Unknown",
                    BoverketProductCategory = BuildingElementCategoryParser.Parse(product, product.Name)
                };

                try
                {
                    layer.LayerGeometry = GeometryParser.Parse(product.EntityLabel, model);
                    layer.LayerGeometry.Weight = WeightCalculator.Calculate(layer.BoverketProductCategory, layer.LayerGeometry);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not calculate volume and weight for " + material.ToString());
                }

                material.Layers = new List<HaiyanMaterialLayer>()
                {
                    layer
                };

                return material;
            }

            var firstMaterial = ifcRelAssociatesMaterials.FirstOrDefault();

            if (ifcRelAssociatesMaterials.Count() > 1)
            {
                Console.WriteLine("Multiple materials for item " + product.Name.ToString());
            }

            var relatedMaterial = firstMaterial.RelatingMaterial as IIfcMaterial;
            var materialSetLayerUsage = firstMaterial.RelatingMaterial as IIfcMaterialLayerSetUsage;
            var materialSetLayer = firstMaterial.RelatingMaterial as IIfcMaterialLayerSet;
            var materialList = firstMaterial.RelatingMaterial as IIfcMaterialList;

            if (materialSetLayerUsage != null)
            {
                material.Layers = GetLayers(materialSetLayerUsage, product, model);
            }

            if (materialSetLayer != null)
            {
                material.Layers = GetLayers(materialSetLayer, product, model);
            }

            if (materialList != null)
            {
                material.Layers = GetLayers(materialList, product, model);
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

            if (material.Layers == null)
            {
                var layer = new HaiyanMaterialLayer
                {
                    Name = material.Name,
                    BoverketProductCategory = BuildingElementCategoryParser.Parse(product, material.Name)
                };
                try
                {
                    layer.LayerGeometry = GeometryParser.Parse(product.EntityLabel, model);
                    layer.LayerGeometry.Weight = WeightCalculator.Calculate(layer.BoverketProductCategory, layer.LayerGeometry);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not calculate volume and weight for " + material.ToString());
                }

                material.Layers = new List<HaiyanMaterialLayer>()
                {
                    layer
                };
            }

            return material;
        }

        private static List<HaiyanMaterialLayer> GetLayers(IIfcMaterialList ifcMaterialList, IIfcProduct product, IfcStore model)
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
                    var materialLayer = new HaiyanMaterialLayer();
                    materialLayer.Name = material.Name;
                    materialLayer.BoverketProductCategory = BuildingElementCategoryParser.Parse(product, material.Name);

                    try
                    {
                        materialLayer.LayerGeometry = GeometryParser.Parse(material.EntityLabel, model);
                        materialLayer.LayerGeometry.Weight = WeightCalculator.Calculate(materialLayer.BoverketProductCategory, materialLayer.LayerGeometry);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Could not calculate volume and weight for " + material.ToString());
                    }

                    layers.Add(materialLayer);
                }

                return layers;
            }

            return default;
        }

        private static List<HaiyanMaterialLayer> GetLayers(IIfcMaterialLayerSetUsage ifcMaterialLayerSetUsage, IIfcProduct product, IfcStore model)
        {
            if (ifcMaterialLayerSetUsage == null)
            {
                return default;
            }

            if (ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.Any())
            {
                return ProcessLayers(ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.ToList(), product, model);
            }

            return default;
        }

        private static List<HaiyanMaterialLayer> GetLayers(IIfcMaterialLayerSet ifcMaterialLayerSet, IIfcProduct product, IfcStore model)
        {
            if (ifcMaterialLayerSet == null)
            {
                return default;
            }

            if (ifcMaterialLayerSet.MaterialLayers.Any())
            {
                return ProcessLayers(ifcMaterialLayerSet.MaterialLayers.ToList(), product, model);
            }

            return default;
        }

        private static List<HaiyanMaterialLayer> ProcessLayers(List<IIfcMaterialLayer> ifcMaterialLayers, IIfcProduct product, IfcStore model)
        {
            var layers = new List<HaiyanMaterialLayer>();

            ifcMaterialLayers.ForEach(x =>
            {
                layers.Add(CreateLayer(x, product, model));
            });

            return layers;
        }

        private static HaiyanMaterialLayer CreateLayer(IIfcMaterialLayer layer, IIfcProduct product, IfcStore model)
        {
            var materialLayer = new HaiyanMaterialLayer();
            materialLayer.Name = layer.Material.Name;
            materialLayer.Thickness = layer.LayerThickness;
            materialLayer.BoverketProductCategory = BuildingElementCategoryParser.Parse(product, layer.Material.Name);

            try
            {
                materialLayer.LayerGeometry = GeometryParser.Parse(layer.EntityLabel, model);

                materialLayer.LayerGeometry.Weight = WeightCalculator.Calculate(materialLayer.BoverketProductCategory, materialLayer.LayerGeometry);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not calculate volume and weight for " + layer.ToString());
            }

            return materialLayer;
        }
    }
}
