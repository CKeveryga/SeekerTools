using Inventor;
using System;

namespace SeekerTools.InventorModels
{
    internal class InvPartDocMapper : InvDocMapper, IInvPartDoc
    {
        private string _length;
        private double _thickness;
        private string _width;
        private string _features = "";
        private BOMQuantityTypeEnum _bomQtyType;
        public InvPartDocMapper(PartDocument partDoc) : base(partDoc)
        {
            Initialise(partDoc);
        }

        public void Initialise(PartDocument partDoc)
        {
            PartCompDef = partDoc.ComponentDefinition;
            PartCompDef.BOMQuantity.GetBaseQuantity(out _bomQtyType, out _);
            BomStructure = PartCompDef.BOMStructure;
            StockType = DesignTrackPropSet["Stock Number"].Value.ToString();
            Material = DesignTrackPropSet["Material"].Value.ToString();
            if (PartCompDef.Type == ObjectTypeEnum.kSheetMetalComponentDefinitionObject) //{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}
            {
                SubType = "SheetMetal";
            }
            else
            {
                SubType = "Part";
            }
            // Get length width thickness
            Parameters partParams = PartCompDef.Parameters;
            double GW_param, GH_param, GT_param, GL_param;
            GW_param = 0;
            GH_param = 0;
            GT_param = 0;
            GL_param = 0;
            if (partParams.Count > 0)
            {
                foreach (Parameter p in partParams)
                {
                    switch (p.Name)
                    {
                        case "G_W":
                            GW_param = Convert.ToDouble(p.Value) / 2.54;
                            GW_param = Math.Round(GW_param, 3);
                            break;
                        case "G_H":
                            GH_param = Convert.ToDouble(p.Value) / 2.54;
                            GH_param = Math.Round(GH_param, 3);
                            break;
                        case "G_T":
                            GT_param = Convert.ToDouble(p.Value) / 2.54;
                            GT_param = Math.Round(GT_param, 3);
                            break;
                        case "G_L":
                            GL_param = Convert.ToDouble(p.Value) / 2.54;
                            GL_param = Math.Round(GL_param, 3);
                            break;
                        default:
                            break;
                    }
                }
            }
            try
            {
                Weight = Math.Round(partDoc.ComponentDefinition.MassProperties.Mass / 0.453592, 2);
            }
            catch
            {
                Weight = double.NaN;
            }
            foreach (PartFeature pf in PartCompDef.Features)
            {
                string feature = Enum.GetName(typeof(ObjectTypeEnum), pf.Type).Replace("FeatureObject", "");
                if (_features.Contains(feature) == false)
                {
                    _features += feature;
                }
            }
            if (SubType == "SheetMetal")
            {
                //get thickness
                if (partParams.Count > 0)
                {
                    foreach (Parameter p in partParams)
                    {
                        switch (p.Name)
                        {
                            case "Thickness":
                                _thickness = Convert.ToDouble(p.Value) / 2.54;
                                _thickness = Math.Round(_thickness, 3);
                                break;
                            default:
                                break;
                        }
                    }
                }
                double tempLength = 0;
                double tempWidth = 0;
                try
                {
                    SheetMetalComponentDefinition smDef = (SheetMetalComponentDefinition)PartCompDef;
                    tempLength = smDef.FlatPattern.Length;
                }
                catch
                { }
                try
                {
                    SheetMetalComponentDefinition smDef = (SheetMetalComponentDefinition)PartCompDef;
                    tempWidth = smDef.FlatPattern.Width;
                }
                catch
                { }
                if (tempLength == 0 || tempWidth == 0)
                { }
                else
                {
                    if (tempWidth > tempLength)
                    {
                        Length = tempWidth;
                        Width = tempLength;
                    }
                    else
                    {
                        Length = tempLength;
                        Width = tempWidth;
                    }
                }
            }
            else if (SubType == "Part")
            {
                Length = GL_param;
                Width = GW_param;
                Thickness = GT_param;
                //Height = GH_param;
            }
        }

        public double Weight { get; private set; }

        public PartComponentDefinition PartCompDef { get; private set; }

        public BOMStructureEnum BomStructure { get; private set; }

        public string SubType { get; private set; }

        public BOMQuantityTypeEnum BomQtyType => _bomQtyType;

        public double Length
        {
            get { return Convert.ToDouble(_length); }
            set { _length = value.ToString(); }
        }

        public double Width
        {
            get { return Convert.ToDouble(_width); }
            set { _width = value.ToString(); }
        }

        public double Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        public string Features { get => _features; }

        public string Material { get; private set; }

        public string StockType { get; private set; }
    }
}
