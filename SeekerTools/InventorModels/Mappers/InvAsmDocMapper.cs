using Inventor;
using System;

namespace SeekerTools.InventorModels
{
    internal class InvAsmDocMapper : InvDocMapper, IInvAsmDoc, IInvModelDoc
    {
        private BOMQuantityTypeEnum _bomQtyType;

        public InvAsmDocMapper(AssemblyDocument asmDoc) : base(asmDoc)
        {
            Initialise(asmDoc);
        }

        private void Initialise(AssemblyDocument asmDoc)
        {
            AsmCompDef = asmDoc.ComponentDefinition;
            AsmCompDef.BOMQuantity.GetBaseQuantity(out _bomQtyType, out _);
            BomStructure = asmDoc.ComponentDefinition.BOMStructure;
            try
            {
                Weight = Math.Round(asmDoc.ComponentDefinition.MassProperties.Mass / 0.453592, 2);
            }
            catch
            {
                Weight = double.NaN;
            }
        }

        public BOMQuantityTypeEnum BomQtyType => _bomQtyType;

        public BOMStructureEnum BomStructure { get; private set; }

        public AssemblyComponentDefinition AsmCompDef { get; private set; }

        public double Weight { get; private set; }
    }
}
